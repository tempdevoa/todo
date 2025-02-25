﻿
namespace Todo.Server.Domain.TodoItemAggregate
{
    public class TodoItem
    {
        public string Id { get; set; }

        public string Title { get; set; }
              
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Only for EF
        /// </summary>
#pragma warning disable CS8618
        public TodoItem()
#pragma warning restore CS8618
        {
        }

        public TodoItem(string id, string title, bool isCompleted)
        {
            Id = id;
            Title = title;
            IsCompleted = isCompleted;
        }

        public void Adopt(TodoItem updatedTodoItem)
        {
            if (Id.Equals(updatedTodoItem.Id))
            {
                Title = updatedTodoItem.Title;
                IsCompleted = updatedTodoItem.IsCompleted;
            }
        }
    }
}