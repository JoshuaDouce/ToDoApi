using System;
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
        private readonly ToDoAppDbContext _context;
        public ToDoItemController(ToDoAppDbContext context)
        {
            _context = context;
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
            var entity = await _context.ToDoItems.SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return NotFound();
            }

            var resource = new ToDoItem {
                Href = Url.Link(nameof(GetToDoItem), new { id = entity.Id }),
                Name = entity.Name,
                IsComplete = entity.IsComplete
            };

            return resource;
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
