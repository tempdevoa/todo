namespace Todo.Server.Domain.TodoItemAggregate
{
    public class TodoItem
    {
        public string Id { get; }

        public string Title { get; }
              
        public bool IsCompleted { get; private set; }

        public TodoItem(string id, string title, bool isCompleted)
        {
            Id = id;
            Title = title;
            IsCompleted = isCompleted;
        }

        public void Complete()
        {
            IsCompleted = true;
        }
    }
}