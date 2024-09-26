using Microsoft.EntityFrameworkCore;
using TodoBackend.Models;

namespace TodoBackend.Repositories
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
           : base(options)
        {

        }

        public DbSet<TodoEntity> TodoItems { get; set; }

    }
}
