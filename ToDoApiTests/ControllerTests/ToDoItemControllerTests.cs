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
        IOptions<PagingOptions> pagingOptions;

        public ToDoItemControllerTests()
        {
            _toDoItemService = new Mock<IToDoItemService>();

            pagingOptions = Options.Create(new PagingOptions() {
                Offset = 0,
                Limit = 2
            });
 
            _toDoItemController = new ToDoItemController(_toDoItemService.Object, pagingOptions);
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

        [Fact]
        public async void Delete_InvalidId_ReturnsNotFound() {
            //Arrange 
            _toDoItemService.Setup(r => r.DeleteToDoItemAsync(1))
                .Returns(Task.FromResult<ToDoItemResponse>(null));

            //Act
            var result = await _toDoItemController.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Delete_ValidId_ReturnsNoContent()
        {
            //Arrange 
            _toDoItemService.Setup(r => r.DeleteToDoItemAsync(1))
                .Returns(Task.FromResult(new ToDoItemResponse()));

            //Act
            var result = await _toDoItemController.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void Put_InvalidId_ReturnsBadRequest() {
            //Arrange 
            var item = new ToDoItem();

            _toDoItemService.Setup(r => r.PutToDoItemAsync(1, item))
                .Returns(Task.FromResult<ToDoItemResponse>(null));

            //Act
            var result = await _toDoItemController.Put(1, item);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void Put_ValidId_ReturnsNoContent()
        {
            //Arrange 
            var item = new ToDoItem();

            _toDoItemService.Setup(r => r.PutToDoItemAsync(1, item))
                .Returns(Task.FromResult(new ToDoItemResponse()));

            //Act
            var result = await _toDoItemController.Put(1, item);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
