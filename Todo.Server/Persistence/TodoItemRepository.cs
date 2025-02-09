using Todo.Server.Domain.TodoItemAggregate;

namespace Todo.Server.Persistence
{
    public interface ITodoItemRepository
    {
        Task FlushAsync();

        Task AddAsync(TodoItem todoItem);

        void Remove(TodoItem todoItem);

        void Update(TodoItem todoItem);

        Task<List<TodoItem>> ToListAsync();

        Task<TodoItem?> FindAsync(object id);
    }

    public class TodoItemRepository
    {
    }
}
