using System.Collections.ObjectModel;
using Todo.Gateways;
using Todo.Models;

namespace Todo.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly ITodoService todoService;

        private string newToDoName;

        public Command LoadTodosCommand { get; }

        public Command AddTodoCommand { get; }
                
        public string NewToDoName
        {
            get { return newToDoName; }
            set { SetProperty(ref newToDoName, value, nameof(NewToDoName)); }
        }

        public ObservableCollection<TodoTask> Todos { get; } = new ObservableCollection<TodoTask>();

        public MainPageViewModel(ITodoService todoService)
        {
            this.todoService = todoService;
            AddTodoCommand = new Command(async () => await AddTodoAsync());

            LoadTodosAsync();
        }

        private async Task LoadTodosAsync()
        {
            var todos = await todoService.GetAllAsync();
            Todos.Clear();
            foreach (var todo in todos)
            {
                Todos.Add(todo);
            }
        }

        private async Task AddTodoAsync()
        {
            var id = NewToDoName.Trim().ToLowerInvariant().Replace(" ", "_");
            var newTodo = new TodoTask(id, NewToDoName, false);
            Todos.Add(newTodo);
            NewToDoName = string.Empty;
            await todoService.AddAsync(newTodo);
            await LoadTodosAsync();
        }

        private async Task ToggleTodoAsync(TodoTask todo)
        {
            //todo.IsCompleted = !todo.IsCompleted;
            await todoService.CompleteAsync(todo);
            await LoadTodosAsync();
        }
    }
}