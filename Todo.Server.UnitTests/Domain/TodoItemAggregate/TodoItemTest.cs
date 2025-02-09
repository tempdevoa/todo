using Todo.Server.Domain.TodoItemAggregate;

namespace Todo.Server.UnitTests.Domain.TodoItemAggregate
{
    public class TodoItemTest
    {
        [Test]
        public void Adopt_WithSameId_ShouldTakeDataFromOther()
        {
            var other = new TodoItem("0", "1", true);

            var testObject = new TodoItem("0", "0", false);
            testObject.Adopt(other);

            Assert.That(testObject.Id, Is.EqualTo("0"));
            Assert.That(testObject.Title, Is.EqualTo(other.Title));
            Assert.That(testObject.IsCompleted, Is.EqualTo(other.IsCompleted));
        }

        [Test]
        public void Adopt_WithOtherId_ShouldNotTakeDataFromOther()
        {
            var other = new TodoItem("1", "1", true);

            var testObject = new TodoItem("0", "0", false);
            testObject.Adopt(other);

            Assert.That(testObject.Id, Is.EqualTo("0"));
            Assert.That(testObject.Title, Is.EqualTo("0"));
            Assert.That(testObject.IsCompleted, Is.False);
        }
    }
}
