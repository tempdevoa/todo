
namespace Todo.Server.Domain.TodoItemAggregate
{
    public class TodoItem
    {
        public string Id { get; }

        public string Title { get; private set; }
              
        public bool IsCompleted { get; private set; }

        public TodoItem(string id, string title, bool isCompleted)
        {
            Id = id;
            Title = title;
            IsCompleted = isCompleted;
        }

        internal void Adopt(TodoItem updatedTodoItem)
        {
            if (Id.Equals(updatedTodoItem.Id))
            {
                Title = updatedTodoItem.Title;
                IsCompleted = updatedTodoItem.IsCompleted;
            }
        }
    }
}