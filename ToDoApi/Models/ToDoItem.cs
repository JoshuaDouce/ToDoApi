namespace ToDoApi.Models
{
    public class ToDoItem : Resource
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }

    }
}
