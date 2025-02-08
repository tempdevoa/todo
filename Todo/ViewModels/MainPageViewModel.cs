﻿using System.Collections.ObjectModel;
using Todo.Gateways;
using Todo.Models;

namespace Todo.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly ITodoItemService todoItemService;
        private string newToDoName = string.Empty;
                
        public string NewToDoName
        {
            get { return newToDoName; }
            set { SetProperty(ref newToDoName, value, nameof(NewToDoName)); }
        }

        public Command AddTodoCommand { get; }

        public Command DeleteTodoCommand { get; }

        public Command CompleteTodoCommand { get; }

        public ObservableCollection<TodoItem> Todos { get; } = new ObservableCollection<TodoItem>();

        public MainPageViewModel(ITodoItemService todoService)
        {
            this.todoItemService = todoService;
            AddTodoCommand = new Command(async () => await AddTodoAsync());
            DeleteTodoCommand = new Command<TodoItem>(async (todoItem) => await DeleteTodoAsync(todoItem.Id));
            CompleteTodoCommand = new Command<TodoItem>(async (todoItem) => await CompleteTodoAsync(todoItem.Id));

            _ = LoadTodosAsync();
        }

        private async Task LoadTodosAsync()
        {
            IsBusy = true;

            var todos = await todoItemService.GetAllAsync();
            if(todos.Count > 0)
            {
                Todos.Clear();
                foreach (var todo in todos)
                {
                    Todos.Add(todo);
                }
            }
            
            IsBusy = false;
        }

        private async Task AddTodoAsync()
        {
            IsBusy = true;
            var id = NewToDoName.Trim().ToLowerInvariant().Replace(" ", "_");
            var newTodo = new TodoItem(id, NewToDoName, false);
            Todos.Add(newTodo);
            NewToDoName = string.Empty;
            await todoItemService.AddAsync(newTodo);
            IsBusy = false;
        }

        private async Task DeleteTodoAsync(string todoId)
        {
            IsBusy = true;
            var foundTodo = Todos.FirstOrDefault(p => p.Id.Equals(todoId));
            if (!string.IsNullOrEmpty(foundTodo.Id))
            {
                Todos.Remove(foundTodo);
                await todoItemService.DeleteAsync(foundTodo);
            }
            IsBusy = false;
        }

        public async Task CompleteTodoAsync(string todoId)
        {
            IsBusy = true;
            var foundTodo = Todos.FirstOrDefault(p => p.Id.Equals(todoId));
            if(!string.IsNullOrEmpty(foundTodo.Id))
            {
                await todoItemService.CompleteAsync(foundTodo);
                await LoadTodosAsync();
            }
            IsBusy = false;
        }
    }
}