using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApi.Models;

namespace ToDoApi
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider service) {
            await AddTestData(service.GetRequiredService<ToDoAppDbContext>());
        }

        public static async Task AddTestData(ToDoAppDbContext context) {
            if (context.ToDoItems.Any())
            {
                return;
            }

            context.ToDoItems.Add(new ToDoItemEntity {
                Name = "Get Groceries",
                IsComplete = true
            });

            context.ToDoItems.Add(new ToDoItemEntity
            {
                Name = "Walk Dogs",
                IsComplete = false
            });

            context.ToDoItems.Add(new ToDoItemEntity
            {
                Name = "Study",
                IsComplete = false
            });

            await context.SaveChangesAsync();
        }
    }
}
