﻿using System.Text.Json.Serialization;

namespace Todo.Models
{
    public struct TodoTask
    {
        public string Id { get; }

        public string Title { get; }

        public bool IsCompleted { get; }

        [JsonConstructor]
        public TodoTask(string id, string title, bool isCompleted)
        {
            Id = id;
            Title = title;
            IsCompleted = isCompleted;
        }
    }
}
