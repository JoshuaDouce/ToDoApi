using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApi.Models;

namespace ToDoApi.Services
{
    public interface IToDoItemService
    {
        Task<IEnumerable<ToDoItem>> GetToDoItemsAsync();
        Task<PagedResults<ToDoItem>> GetToDoItemsAsync(PagingOptions options);
        Task<ToDoItem> GetToDoItemAsync(long id);
        Task<ToDoItemResponse> PostToDoItemAsync(ToDoItem toDoItem);
        Task<ToDoItemResponse> PutToDoItemAsync(long id, ToDoItem toDoItem);
        Task<ToDoItemResponse> DeleteToDoItemAsync(long id);
    }
}
