using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Services;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    //This attribute indicates that the controller responds to web API requests.
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly IToDoItemService _toDoItemService;
        public ToDoItemController(IToDoItemService service)
        {
            _toDoItemService = service;
        }

        // GET: api/ToDo
        [HttpGet(Name = nameof(GetToDoItems))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Collection<ToDoItem>>> GetToDoItems()
        {
            var items = await _toDoItemService.GetToDoItemsAsync();

            var collection = new Collection<ToDoItem>
            {
                Self = Link.ToCollection(nameof(GetToDoItems)),
                Value = items.ToArray()
            };

            return collection;
        }

        //GET: api/ToDo/{queryString}
        [HttpGet("pagedToDoItems",Name = nameof(GetPagedToDoItems))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Collection<ToDoItem>>> GetPagedToDoItems([FromQuery] PagingOptions pagingOptions = null)
        {
            var items = await _toDoItemService.GetToDoItemsAsync(pagingOptions);

            var collection = new PagedCollection<ToDoItem>
            {
                Self = Link.ToCollection(nameof(GetToDoItems)),
                Value = items.Items.ToArray(),
                Size = items.TotalSize,
                Offset = pagingOptions.Offset,
                Limit = pagingOptions.Limit
            };

            return collection;
        }


        // GET api/ToDo/5
        [HttpGet("{id}", Name = nameof(GetToDoItem))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(long id)
        {
            var room = await _toDoItemService.GetToDoItemAsync(id);

            if (room != null)
            {
                return room;
            }

            return NotFound();
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ToDoItem>> PostToDoItem(ToDoItem item)
        {
            var response = await _toDoItemService.PostToDoItemAsync(item);

            return CreatedAtAction(nameof(GetToDoItem), new { id = response.Id}, response.Item);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put(long id, ToDoItem item)
        {
            var response = await _toDoItemService.PutToDoItemAsync(id, item);

            if (response == null) return BadRequest();

            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _toDoItemService.DeleteToDoItemAsync(id);

            if (response == null) return NotFound();

            return NoContent();
        }
    }
}
