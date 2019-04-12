using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApi.Models;

namespace ToDoApi.Services
{
    public class DefaultToDoItemService : IToDoItemService
    {
        private readonly ToDoAppDbContext _context;

        public DefaultToDoItemService(ToDoAppDbContext context)
        {
            _context = context;
        }

        public async Task<ToDoItem> GetToDoItemAsync(long id)
        {
            var entity = await _context.ToDoItems.SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return null;
            }

            return new ToDoItem
            {
                Href = null, //Url.Link(nameof(GetToDoItem), new { id = entity.Id }),
                Name = entity.Name,
                IsComplete = entity.IsComplete
            };

        }
    }
}
