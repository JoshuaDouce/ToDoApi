using Newtonsoft.Json;

namespace ToDoApi.Models
{
    public abstract class Resource : Link
    {
        [JsonIgnore]
        public Link Self { get; set; }
    }
}
