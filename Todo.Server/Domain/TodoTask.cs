namespace Todo.Server.Domain
{
    public class TodoTask
    {
        public string Id { get; }

        public string Title { get; }

        public bool IsCompleted { get; }

        public TodoTask(string id, string title, bool isCompleted)
        {
            Id = id;
            Title = title;
            IsCompleted = isCompleted;
        }
    }
}
