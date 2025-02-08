using System.Text.Json.Serialization;

namespace Todo.Models
{
    public struct TodoItem
    {
        public string Id { get; }

        public string Title { get; }

        public bool IsCompleted { get; }

        public bool CanBeCompleted => !IsCompleted;

        [JsonConstructor]
        public TodoItem(string id, string title, bool isCompleted)
        {
            Id = id;
            Title = title;
            IsCompleted = isCompleted;
        }
    }
}
