using System.ComponentModel.DataAnnotations;

namespace ToDoApi.Models
{
    public class PagingOptions
    {
        [Range(1, 99999, ErrorMessage = "Offset must be greater than 0")]
        public int? Offset { get; set; }
        [Range(1, 100, ErrorMessage = "Offset must be gbetween 1 and 100")]
        public int? Limit { get; set; }
        public PagingOptions Replace(PagingOptions options) {
            return new PagingOptions
            {
                Offset = options.Offset ?? Offset,
                Limit = options.Limit ?? Limit
            };
        }
    }
}
