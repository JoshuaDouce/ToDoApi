using ToDoApi.Infrastructure;

namespace ToDoApi.Models
{
    public class ToDoItem : Resource
    {
        [Sortable(Default = true)]
        [Searchable]
        public string Name { get; set; }
        public bool IsComplete { get; set; }

    }
}
