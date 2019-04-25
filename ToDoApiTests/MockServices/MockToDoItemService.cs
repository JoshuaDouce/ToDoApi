using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApi.Models;
using ToDoApi.Services;

namespace ToDoApiTests.MockServices
{
    class MockToDoItemService : IToDoItemService
    {
        private readonly List<ToDoItem> _toDoItems;
        public MockToDoItemService()
        {
            _toDoItems = new List<ToDoItem>()
            {
                new ToDoItem()
                {
                    Name = "Item One",
                    IsComplete = false
                },
                new ToDoItem()
                {
                    Name = "Item Two",
                    IsComplete = false
                },
                new ToDoItem()
                {
                    Name = "Item Three",
                    IsComplete = true
                },
                new ToDoItem()
                {
                    Name = "Item Four",
                    IsComplete = false
                },
                new ToDoItem()
                {
                    Name = "Item Five",
                    IsComplete = true
                }
            };
        }

        public Task<ToDoItemResponse> DeleteToDoItemAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ToDoItem> GetToDoItemAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedResults<ToDoItem>> GetToDoItemsAsync(SortOptions<ToDoItem, ToDoItemEntity> sortOptions, PagingOptions pagingOptions, SearchOptions<ToDoItem, ToDoItemEntity> searchOptions)
        {
            throw new System.NotImplementedException();
        }

        public Task<ToDoItemResponse> PostToDoItemAsync(ToDoItem toDoItem)
        {
            throw new System.NotImplementedException();
        }

        public Task<ToDoItemResponse> PutToDoItemAsync(long id, ToDoItem toDoItem)
        {
            throw new System.NotImplementedException();
        }
    }
}
