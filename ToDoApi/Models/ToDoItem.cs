using ToDoApi.Infrastructure;

namespace ToDoApi.Models
{
    public class ToDoItem : Resource
    {
        [Sortable]
        public string Name { get; set; }

        public bool IsComplete { get; set; }

    }
}
