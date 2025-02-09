using Todo.Server.Domain.TodoItemAggregate;

namespace Todo.Server.UnitTests.Domain.TodoItemAggregate
{
    public class TodoItemBuilder
    {
        public static TodoItemBuilder New()
        {
            return new TodoItemBuilder();
        }

        private string id;
        private string title;
        private bool isCompleted;

        private TodoItemBuilder()
        {
            id = Guid.NewGuid().ToString();
            title = $"Title {id}";
            isCompleted = false;
        }

        public TodoItem Build()
        {
            return new TodoItem(id, title, isCompleted);
        }
    }
}
