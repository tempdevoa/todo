namespace Todo.Server.Domain
{
    public class TodoTask
    {
        public string Id { get; }

        public string Title { get; }

        public bool IsCompleted { get; private set; }

        public TodoTask(string id, string title, bool isCompleted)
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
