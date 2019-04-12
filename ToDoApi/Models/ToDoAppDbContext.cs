using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Models
{
    public class ToDoAppDbContext : DbContext
    {
        public ToDoAppDbContext(DbContextOptions<ToDoAppDbContext> options) : base(options)
        {

        }

        public DbSet<ToDoItemEntity> ToDoItems { get; set; }
    }
}
