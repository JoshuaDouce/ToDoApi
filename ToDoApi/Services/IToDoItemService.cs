using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApi.Models;

namespace ToDoApi.Services
{
    public interface IToDoItemService
    {
        Task<ToDoItem> GetToDoItemAsync(long id);
    }
}
