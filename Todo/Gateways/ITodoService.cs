using Todo.Models;

namespace Todo.Gateways
{
    public interface ITodoService
    {
        Task<List<TodoTask>> GetAllAsync();

        Task AddAsync(TodoTask todo);
        
        Task CompleteAsync(TodoTask todo);
    }
}
