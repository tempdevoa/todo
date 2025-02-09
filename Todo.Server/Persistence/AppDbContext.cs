using Microsoft.EntityFrameworkCore;
using Todo.Server.Domain.TodoItemAggregate;

namespace Todo.Server.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
