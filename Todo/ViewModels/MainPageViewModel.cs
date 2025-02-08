using System.Collections.ObjectModel;
using Todo.Gateways;
using Todo.Models;

namespace Todo.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly ITodoService todoService;

        private string newToDoName;
                
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

            _ = LoadTodosAsync();
        }

        private async Task LoadTodosAsync()
        {
            IsBusy = true;
            var todos = await todoService.GetAllAsync();
            Todos.Clear();
            foreach (var todo in todos)
            {
                Todos.Add(todo);
            }
            IsBusy = false;
        }

        private async Task AddTodoAsync()
        {
            IsBusy = true;
            var id = NewToDoName.Trim().ToLowerInvariant().Replace(" ", "_");
            var newTodo = new TodoTask(id, NewToDoName, false);
            Todos.Add(newTodo);
            NewToDoName = string.Empty;
            await todoService.AddAsync(newTodo);
            await LoadTodosAsync();
            IsBusy = false;
        }

        public async Task CompleteTodo(string todoId)
        {
            IsBusy = true;
            var foundTodo = Todos.FirstOrDefault(p => p.Id.Equals(todoId));
            if(!string.IsNullOrEmpty(foundTodo.Id))
            {
                await todoService.CompleteAsync(foundTodo);
                await LoadTodosAsync();
            }
            IsBusy = false;
        }
    }
}