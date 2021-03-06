﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Services;
using System.Linq;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    //This attribute indicates that the controller responds to web API requests.
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly IToDoItemService _toDoItemService;
        private readonly PagingOptions _defaultPagingOptions;

        public ToDoItemController(IToDoItemService service, IOptions<PagingOptions> defaultPagingOptions)
        {
            _toDoItemService = service;
            _defaultPagingOptions = defaultPagingOptions.Value;
        }

        // GET: api/ToDo
        [HttpGet(Name = nameof(GetToDoItems))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Collection<ToDoItem>>> GetToDoItems(
            [FromQuery] SortOptions<ToDoItem, ToDoItemEntity> sortOptions,
            [FromQuery] SearchOptions<ToDoItem, ToDoItemEntity> searchOptions,
            [FromQuery] PagingOptions pagingOptions = null)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var items = await _toDoItemService.GetToDoItemsAsync(sortOptions, pagingOptions, searchOptions);

            var collection = PagedCollection<ToDoItem>.Create(
                Link.ToCollection(nameof(GetToDoItems)),
                items.Items.ToArray(),
                items.TotalSize,
                pagingOptions);

            return collection;
        }

        // GET api/ToDo/5
        [HttpGet("{id}", Name = nameof(GetToDoItem))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(long id)
        {
            var toDoItem = await _toDoItemService.GetToDoItemAsync(id);

            if (toDoItem != null)
            {
                return toDoItem;
            }

            return NotFound();
        }

        // POST api/<controller>
        [HttpPost(Name = nameof(PostToDoItem))]
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
