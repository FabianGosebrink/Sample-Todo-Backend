using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Repositories
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
