using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApi.Models;

namespace ToDoApi.Services
{
    public class DefaultToDoItemService : IToDoItemService
    {
        private readonly ToDoAppDbContext _context;
        private readonly IMapper _mapper;

        public DefaultToDoItemService(ToDoAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ToDoItem> GetToDoItemAsync(long id)
        {
            var entity = await _context.ToDoItems.SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return null;
            }

            return _mapper.Map<ToDoItem>(entity);

        }

        public async Task<ToDoItemReponse> PostToDoItemAsync(ToDoItem toDoItem)
        {
            var entity = _mapper.Map<ToDoItemEntity>(toDoItem);

            _context.ToDoItems.Add(entity);
            await _context.SaveChangesAsync();

            var rewritten = _mapper.Map<ToDoItem>(entity);

            return new ToDoItemReponse { Id = entity.Id, Item = rewritten};
        }

        public async Task<ToDoItemReponse> PutToDoItemAsync(long id, ToDoItem toDoItem)
        {
            var entity = await _context.ToDoItems.SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null) return null;

            entity.IsComplete = toDoItem.IsComplete;
            entity.Name = toDoItem.Name;

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var rewritten = _mapper.Map<ToDoItem>(entity);

            return new ToDoItemReponse { Id = entity.Id, Item = rewritten };
        }

        public async Task<ToDoItemReponse> DeleteToDoItemAsync(long id)
        {
            var entity = await _context.ToDoItems.SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null) return null;

            _context.ToDoItems.Remove(entity);
            await _context.SaveChangesAsync();

            return new ToDoItemReponse { Id = entity.Id, Item = null };
        }
    }
}
