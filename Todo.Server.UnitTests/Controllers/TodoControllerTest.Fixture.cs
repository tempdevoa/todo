using Microsoft.AspNetCore.Mvc;
using Moq;
using Todo.Server.Controllers;
using Todo.Server.Domain.TodoItemAggregate;
using Todo.Server.Persistence;
using Todo.Server.UnitTests.Domain.TodoItemAggregate;

namespace Todo.Server.UnitTests.Controllers
{
    public partial class TodoControllerTest
    {
        private Fixture fixture;

        [SetUp]
        public void Setup()
        {
            fixture = new Fixture();
        }

        private class Fixture
        {
            private readonly Mock<ITodoItemRepository> repositoryMock = new Mock<ITodoItemRepository>(MockBehavior.Loose);

            public IReadOnlyCollection<TodoItem> PersistedTodoItems { get; }

            public Fixture()
            {
                PersistedTodoItems = new List<TodoItem>
                {
                    TodoItemBuilder.New().Build(),
                    TodoItemBuilder.New().Build()
                };

                repositoryMock.Setup(m => m.ToListAsync()).ReturnsAsync([.. PersistedTodoItems]);
                repositoryMock.Setup(m => m.FindAsync(It.IsAny<string>())).ReturnsAsync((string id) => PersistedTodoItems.FirstOrDefault(p => p.Id.Equals(id)));
            }

            public void SetupEmptyRepository()
            {
                repositoryMock.Setup(m => m.ToListAsync()).ReturnsAsync(new List<TodoItem>());
            }

            public TodoController CreateTestObject()
            {
                return new TodoController(repositoryMock.Object);
            }

            public void AssertResultContainsItems(IActionResult result, int countOfExpectedItems)
            {
                Assert.That(result, Is.TypeOf<OkObjectResult>());
                var resultOkObjectResult = result as OkObjectResult;
                var resultTodoItems = resultOkObjectResult?.Value as List<TodoItem>;

                Assert.That(resultTodoItems, Is.Not.Null);
                Assert.That(resultTodoItems.Count, Is.EqualTo(countOfExpectedItems));
            }

            public void AssertResultContainsCreatedItems(IActionResult result, int countOfExpectedItems)
            {
                Assert.That(result, Is.TypeOf<CreatedResult>());
                var resultCreatedResult = result as CreatedResult;
                var resultTodoItem = resultCreatedResult?.Value as TodoItem;

                Assert.That(resultTodoItem, Is.Not.Null);
            }

            public void AssertResultIsBadRequest(IActionResult result)
            {
                Assert.That(result, Is.TypeOf<BadRequestResult>());
            }

            public void AssertResultIsNoContentResult(IActionResult result)
            {
                Assert.That(result, Is.TypeOf<NoContentResult>());
            }

            public void AssertResultIsNotFoundResult(IActionResult result)
            {
                Assert.That(result, Is.TypeOf<NotFoundResult>());
            }

            public void AssertResultIsOkResult(IActionResult result)
            {
                Assert.That(result, Is.TypeOf<OkResult>());
            }

            public void AssertResultIsConflictResult(IActionResult result)
            {
                Assert.That(result, Is.TypeOf<ConflictResult>());
            }

            public void AssertRepositoryAddAsyncInvoked()
            {
                repositoryMock.Verify(m => m.AddAsync(It.IsAny<TodoItem>()), Times.Once);
                repositoryMock.Verify(m => m.FlushAsync(), Times.Once);
            }

            public void AssertRepositoryRemoveInvoked()
            {
                repositoryMock.Verify(m => m.Remove(It.IsAny<TodoItem>()), Times.Once);
                repositoryMock.Verify(m => m.FlushAsync(), Times.Once);
            }

            public void AssertRepositoryUpdateInvoked()
            {
                repositoryMock.Verify(m => m.Update(It.IsAny<TodoItem>()), Times.Once);
                repositoryMock.Verify(m => m.FlushAsync(), Times.Once);
            }
        }
    }
}
