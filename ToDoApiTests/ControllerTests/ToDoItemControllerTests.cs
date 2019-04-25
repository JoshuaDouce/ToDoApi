using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ToDoApi.Controllers;
using ToDoApi.Models;
using ToDoApi.Services;
using ToDoApiTests.MockServices;
using Xunit;

namespace ToDoApiTests.ControllerTests
{
    public class ToDoItemControllerTests
    {
        ToDoItemController _toDoItemController;
        IToDoItemService _toDoItemService;

        public ToDoItemControllerTests()
        {
            _toDoItemService = new MockToDoItemService();
            var options = Options.Create(new PagingOptions() {
                Offset = 0,
                Limit = 5
            });
            _toDoItemController = new ToDoItemController(_toDoItemService, options);
        }

        [Fact]
        public async void GetById_UnknownId_ReturnsNotFound() {
            //Act
            var result = await _toDoItemController.GetToDoItem(10);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void GetById_ExistingId_ReturnsCorrectToDoItem()
        {
            //Act
            var result = await _toDoItemController.GetToDoItem(3);

            // Assert
            Assert.IsType<ToDoItem>(result.Value);
            Assert.Equal("Item Three", result.Value.Name);
            Assert.True(result.Value.IsComplete);
        }
    }
}
