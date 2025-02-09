using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Todo.Server.Domain.TodoItemAggregate;

namespace Todo.Server.Persistence
{
    public interface IAppDbContext
    {
        DbSet<TodoItem> TodoItems { get; }

        EntityEntry Update(object todoItem);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
