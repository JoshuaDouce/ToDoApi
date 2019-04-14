﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ToDoApi.Infrastructure;
using ToDoApi.Models;

namespace ToDoApi.Filters
{
    public class LinkRewritingFilter : IAsyncResultFilter
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        public LinkRewritingFilter(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }
        public Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var asObjectResult = context.Result as ObjectResult;

            bool shouldSkip = asObjectResult?.StatusCode >= 400 || 
                asObjectResult?.Value == null || 
                asObjectResult?.Value as Resource == null;

            if (shouldSkip) return next();

            var rewriter = new LinkRewriter(_urlHelperFactory.GetUrlHelper(context));

            RewriteAllLinks(asObjectResult.Value, rewriter);

            return next();
        }

        private static void RewriteAllLinks(object model, LinkRewriter rewriter)
        {
            if (model == null) return;

            var allProperties = model.GetType().GetTypeInfo().GetAllProperties().Where(p => p.CanRead).ToArray();

            var linkProperties = allProperties.Where(p => p.CanWrite && p.PropertyType == typeof(Link));

            foreach (var linkProp in linkProperties)
            {
                var rewritten = rewriter.LinkRewrite(linkProp.GetValue(model) as Link);

                if (rewritten == null) continue;

                linkProp.SetValue(model, rewritten);

                //Specila handling of self
                if (linkProp.Name == nameof(Resource.Self))
                {
                    allProperties.SingleOrDefault(p => p.Name == nameof(Resource.Href))?.SetValue(model, rewritten.Href);

                    allProperties.SingleOrDefault(p => p.Name == nameof(Resource.Relations))?.SetValue(model, rewritten.Relations);

                    allProperties.SingleOrDefault(p => p.Name == nameof(Resource.Method))?.SetValue(model, rewritten.Method);
                }
            }

            var arrayProperties = allProperties.Where(p => p.PropertyType.IsArray);
            RewriteLinksInArrays(arrayProperties, model, rewriter);

            var objectProperties = allProperties
                .Except(linkProperties)
                .Except(arrayProperties);
            RewriteLinksInNestedObjects(objectProperties, model, rewriter);
        }

        private static void RewriteLinksInNestedObjects(
            IEnumerable<PropertyInfo> objectProperties,
            object model,
            LinkRewriter rewriter)
        {
            foreach (var objectProperty in objectProperties)
            {
                if (objectProperty.PropertyType == typeof(string))
                {
                    continue;
                }

                var typeInfo = objectProperty.PropertyType.GetTypeInfo();
                if (typeInfo.IsClass)
                {
                    RewriteAllLinks(objectProperty.GetValue(model), rewriter);
                }
            }
        }

        private static void RewriteLinksInArrays(
            IEnumerable<PropertyInfo> arrayProperties,
            object model,
            LinkRewriter rewriter)
        {

            foreach (var arrayProperty in arrayProperties)
            {
                var array = arrayProperty.GetValue(model) as Array ?? new Array[0];

                foreach (var element in array)
                {
                    RewriteAllLinks(element, rewriter);
                }
            }
        }
    }
}
