﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ToDoApi.Infrastructure
{
    internal class SortOptionsProcessor<T, TEntity>
    {
        private readonly string[] _orderBy;

        public SortOptionsProcessor(string[] orderBy)
        {
            _orderBy = orderBy;
        }

        public IEnumerable<SortTerm> GetAllTerms()
        {
            if (_orderBy == null) yield break;

            foreach (var term in _orderBy)
            {
                var tokens = term.Split(' ');

                if (tokens.Length == 0)
                {
                    yield return new SortTerm { Name = term };
                    continue;
                }

                var descending = tokens.Length > 1 && tokens[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

                yield return new SortTerm
                {
                    Name = tokens[0],
                    Descending = descending
                };
            }
        }

        public IEnumerable<SortTerm> GetValidTerms() {
            var terms = GetAllTerms();

            if (!terms.Any()) yield break;

            var declaredTerms = GetTermsFromModel();

            foreach (var term in terms)
            {
                var declaredTerm = declaredTerms.SingleOrDefault(x => x.Name.Equals(term.Name, StringComparison.OrdinalIgnoreCase));

                if (declaredTerm == null) continue;

                yield return new SortTerm {
                    Name = declaredTerm.Name,
                    Descending = term.Descending
                };
            }
        }

        private static IEnumerable<SortTerm> GetTermsFromModel()
            => typeof(T).GetTypeInfo()
            .DeclaredProperties
            .Where(p => p.GetCustomAttributes<SortableAttribute>().Any())
            .Select(p => new SortTerm { Name = p.Name});

        internal IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            var terms = GetValidTerms().ToArray();
            if (!terms.Any()) return query;

            var modifiedQuery = query;
            var useThenBy = false;

            foreach (var term in terms)
            {
                var propertyInfo = ExpressionHelper
                    .GetPropertyInfo<TEntity>(term.Name);
                var obj = ExpressionHelper.Parameter<TEntity>();

                var key = ExpressionHelper
                    .GetPropertyExpression(obj, propertyInfo);
                var keySelector = ExpressionHelper
                    .GetLambda(typeof(TEntity), propertyInfo.PropertyType, obj, key);

                modifiedQuery = ExpressionHelper
                    .CallOrderByOrThenBy(
                        modifiedQuery, useThenBy, term.Descending, propertyInfo.PropertyType, keySelector);

                useThenBy = true;
            }

            return modifiedQuery;
        }
    }
}