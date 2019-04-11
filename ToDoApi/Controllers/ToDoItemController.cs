using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    //This attribute indicates that the controller responds to web API requests.
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly ToDoItemContext _toDoItemContext;

        public ToDoItemController(ToDoItemContext toDoItemContext)
        {
            _toDoItemContext = toDoItemContext;

            if (_toDoItemContext.ToDoItems.Count() == 0)
            {
                _toDoItemContext.ToDoItems.Add(new ToDoItem { Name = "Item 1"});
                _toDoItemContext.SaveChanges();
            }
        }

        // GET: api/ToDO
        [HttpGet(Name = nameof(GetToDoItems))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            return await _toDoItemContext.ToDoItems.ToListAsync();
        }

        // GET api/ToDo/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(long id)
        {
            var todoItem = await _toDoItemContext.ToDoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
