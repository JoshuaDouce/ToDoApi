using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;

namespace ToDoApi.Infrastructure
{
    public class LinkRewriter
    {
        private readonly IUrlHelper _urlHelper;

        public LinkRewriter(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public Link LinkRewrite(Link original) {
            if (original == null) return null;

            return new Link()
            {
                Href = _urlHelper.Link(original.RouteName, original.Values),
                Method = original.Method,
                Relations = original.Relations
            };
        }
    }
}
