using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using ToDoApi.Controllers;
using ToDoApi.Models;
using ToDoApi.Services;
using Xunit;

namespace ToDoApiTests.ControllerTests
{
    public class ToDoItemControllerTests
    {
        ToDoItemController _toDoItemController;
        private Mock<IToDoItemService> _toDoItemService;

        public ToDoItemControllerTests()
        {
            _toDoItemService = new Mock<IToDoItemService>();

            var options = Options.Create(new PagingOptions() {
                Offset = 0,
                Limit = 5
            });

            _toDoItemController = new ToDoItemController(_toDoItemService.Object, options);
        }

        [Fact]
        public async void GetById_UnknownId_ReturnsNotFound() {
            //Arrange
            _toDoItemService.Setup(r => r.GetToDoItemAsync(10))
                .Returns(Task.FromResult<ToDoItem>(null));

            //Act
            var result = await _toDoItemController.GetToDoItem(10);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void GetById_ExistingId_ReturnsCorrectToDoItem()
        {
            //Arrange
            _toDoItemService.Setup(r => r.GetToDoItemAsync(3))
                .Returns(Task.FromResult(
                    new ToDoItem {
                        Name = "Item Three",
                        IsComplete = true
                    }));

            //Act
            var result = await _toDoItemController.GetToDoItem(3);

            // Assert
            Assert.IsType<ToDoItem>(result.Value);
            Assert.Equal("Item Three", result.Value.Name);
            Assert.True(result.Value.IsComplete);
        }
    }
}
