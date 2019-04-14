using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Services;

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

        // GET: api/ToDO
        [HttpGet(Name = nameof(GetToDoItems))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            throw new NotImplementedException();
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
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
