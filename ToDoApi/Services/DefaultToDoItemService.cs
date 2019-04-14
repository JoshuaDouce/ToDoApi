using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApi.Models;

namespace ToDoApi.Services
{
    public class DefaultToDoItemService : IToDoItemService
    {
        private readonly ToDoAppDbContext _context;
        private readonly IConfigurationProvider _mappingConfig;

        public DefaultToDoItemService(ToDoAppDbContext context, IMapper mapper, IConfigurationProvider configurationProvider)
        {
            _context = context;
            _mappingConfig = configurationProvider;
        }

        public async Task<IEnumerable<ToDoItem>> GetToDoItemsAsync()
        {
            var query = _context.ToDoItems.ProjectTo<ToDoItem>(_mappingConfig);

            return await query.ToArrayAsync();
        }

        public async Task<ToDoItem> GetToDoItemAsync(long id)
        {
            var entity = await _context.ToDoItems.SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return null;
            }

            var mapper = _mappingConfig.CreateMapper();
            return mapper.Map<ToDoItem>(entity);

        }

        public async Task<ToDoItemResponse> PostToDoItemAsync(ToDoItem toDoItem)
        {
            var mapper = _mappingConfig.CreateMapper();

            var entity = mapper.Map<ToDoItemEntity>(toDoItem);

            _context.ToDoItems.Add(entity);
            await _context.SaveChangesAsync();

            var rewritten = mapper.Map<ToDoItem>(entity);

            return new ToDoItemResponse { Id = entity.Id, Item = rewritten};
        }

        public async Task<ToDoItemResponse> PutToDoItemAsync(long id, ToDoItem toDoItem)
        {
            var mapper = _mappingConfig.CreateMapper();

            var entity = await _context.ToDoItems.SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null) return null;

            entity.IsComplete = toDoItem.IsComplete;
            entity.Name = toDoItem.Name;

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var rewritten = mapper.Map<ToDoItem>(entity);

            return new ToDoItemResponse { Id = entity.Id, Item = rewritten };
        }

        public async Task<ToDoItemResponse> DeleteToDoItemAsync(long id)
        {
            var entity = await _context.ToDoItems.SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null) return null;

            _context.ToDoItems.Remove(entity);
            await _context.SaveChangesAsync();

            return new ToDoItemResponse { Id = entity.Id, Item = null };
        }


    }
}
