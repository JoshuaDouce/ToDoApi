using System;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace ToDoApi.Models
{
    public class PagedCollection<T> : Collection<T>
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Offset { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Limit { get; set; }

        public int Size { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link First { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link Previous { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link Next { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Link Last { get; set; }

        public static PagedCollection<T> Create(Link self, T[] items, int size, PagingOptions pagingOptions) {
            return new PagedCollection<T>
            {
                Self = self,
                Value = items, 
                Size = size,
                Offset = pagingOptions.Offset,
                Limit = pagingOptions.Limit,
                First = self,
                Next = GetNextLink(self, size, pagingOptions),
                Previous = GetPreviousLink(self, size, pagingOptions),
                Last = GetLastLink(self, size, pagingOptions)
            };
        }

        private static Link GetLastLink(Link self, int size, PagingOptions pagingOptions)
        {
            throw new NotImplementedException();
        }

        private static Link GetPreviousLink(Link self, int size, PagingOptions pagingOptions)
        {
            throw new NotImplementedException();
        }

        private static Link GetNextLink(Link self, int size, PagingOptions pagingOptions)
        {
            if (pagingOptions?.Offset == null) return null;
            if (pagingOptions?.Limit == null) return null;

            var limit = pagingOptions.Limit.Value;
            var offset = pagingOptions.Offset.Value;

            var nextPage = offset + limit;

            if (nextPage >= size)
            {
                return null;
            }

            var parameters = new RouteValueDictionary(self.Values) {
                ["limit"] = limit,
                ["offset"] = nextPage
            };

            var newLink = ToCollection(self.RouteName, parameters);

            return newLink;
        }
    }
}
