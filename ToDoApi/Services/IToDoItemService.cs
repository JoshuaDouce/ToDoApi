using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApi.Models;

namespace ToDoApi.Services
{
    public interface IToDoItemService
    {
        Task<ToDoItem> GetToDoItemAsync(long id);
        Task<ToDoItemReponse> PostToDoItemAsync(ToDoItem toDoItem);
        Task<ToDoItemReponse> PutToDoItemAsync(long id, ToDoItem toDoItem);
        Task<ToDoItemReponse> DeleteToDoItemAsync(long id);
    }
}
