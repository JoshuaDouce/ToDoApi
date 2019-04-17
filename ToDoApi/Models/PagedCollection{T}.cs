using System;
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
            throw new NotImplementedException();
        }
    }
}
