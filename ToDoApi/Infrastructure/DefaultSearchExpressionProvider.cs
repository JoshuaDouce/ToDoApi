using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ToDoApi.Infrastructure
{
    public class DefaultSearchExpressionProvider : ISearchExpressionProvider
    {
        public virtual ConstantExpression GetValue(string input) => Expression.Constant(input);
    }
}
