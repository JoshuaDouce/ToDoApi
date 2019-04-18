using ToDoApi.Infrastructure;

namespace ToDoApi.Models
{
    public class ToDoItem : Resource
    {
        [Sortable]
        public string Name { get; set; }

        [Sortable]
        public bool IsComplete { get; set; }

    }
}
