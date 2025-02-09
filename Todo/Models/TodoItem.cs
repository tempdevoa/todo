using System.Text.Json.Serialization;

namespace Todo.Models
{
    public struct TodoItem
    {
        public string Id { get; }

        public string Title { get; }

        public bool IsCompleted { get; }

        [JsonConstructor]
        public TodoItem(string id, string title, bool isCompleted)
        {
            Id = id;
            Title = title;
            IsCompleted = isCompleted;
        }

        public TodoItem Rename(string newTitle)
        {
            return new TodoItem(Id, newTitle, IsCompleted);
        }

        public TodoItem Complete()
        {
            return new TodoItem(Id, Title, true);
        }
    }
}
