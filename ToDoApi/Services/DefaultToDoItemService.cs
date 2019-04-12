using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    }
}
