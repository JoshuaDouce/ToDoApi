using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApi.Models;
using ToDoApi.Services;

namespace ToDoApiTests.MockServices
{
    class MockToDoItemService : IToDoItemService
    {
        private readonly List<ToDoItemEntity> _toDoItems;
        public MockToDoItemService()
        {
            _toDoItems = new List<ToDoItemEntity>()
            {
                new ToDoItemEntity()
                {
                    Id = 1,
                    Name = "Item One",
                    IsComplete = false
                },
                new ToDoItemEntity()
                {
                    Id = 2,
                    Name = "Item Two",
                    IsComplete = false
                },
                new ToDoItemEntity()
                {
                    Id = 3,
                    Name = "Item Three",
                    IsComplete = true
                },
                new ToDoItemEntity()
                {
                    Id = 4,
                    Name = "Item Four",
                    IsComplete = false
                },
                new ToDoItemEntity()
                {
                    Id = 5,
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
            var entity = _toDoItems.Where(a => a.Id == id).FirstOrDefault();

            var item = new ToDoItem()
            {
                Name = entity.Name,
                IsComplete = entity.IsComplete
            };

            return Task.FromResult(item);
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
