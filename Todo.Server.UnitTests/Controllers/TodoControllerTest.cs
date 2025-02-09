using Todo.Server.UnitTests.Domain.TodoItemAggregate;

namespace Todo.Server.UnitTests.Controllers
{
    public partial class TodoControllerTest
    {
        [Test]
        public async Task GetAsync_WithTodoItemsInRepository_ShouldReturnCorrectItems()
        {
            var testObject = fixture.CreateTestObject();

            var result = await testObject.GetAsync();

            fixture.AssertResultContainsItems(result, fixture.PersistedTodoItems.Count);
        }

        [Test]
        public async Task GetAsync_WithEmptyRepository_ShouldReturnCorrectItems()
        {
            fixture.SetupEmptyRepository();

            var testObject = fixture.CreateTestObject();

            var result = await testObject.GetAsync();

            fixture.AssertResultContainsItems(result, 0);
        }

        [Test]
        public async Task AddAsync_WithTodoItem_ShouldReturnCorrectItem()
        {
            var testObject = fixture.CreateTestObject();

            var result = await testObject.AddAsync(TodoItemBuilder.New().Build());

            fixture.AssertResultContainsCreatedItems(result, 1);
        }

        [Test]
        public async Task AddAsync_WithTodoItem_ShouldInvokeRepositoryCorrectly()
        {
            var testObject = fixture.CreateTestObject();

            await testObject.AddAsync(TodoItemBuilder.New().Build());

            fixture.AssertRepositoryAddAsyncInvoked();
        }

        [Test]
        public async Task AddAsync_WithAlreadyExistingTodoItem_ShouldReturnsss()
        {
            var testObject = fixture.CreateTestObject();

            var result = await testObject.AddAsync(fixture.PersistedTodoItems.First());

            fixture.AssertResultIsConflictResult(result);
        }

        [Test]
        public async Task AddAsync_WithoutTodoItem_ShouldReturnCorrectResponse()
        {
            var testObject = fixture.CreateTestObject();

            var result = await testObject.AddAsync(null);

            fixture.AssertResultIsBadRequest(result);
        }

        [Test]
        public async Task DeleteAsync_WithPersistedTodoItem_ShouldReturnNoContentResult()
        {
            var testObject = fixture.CreateTestObject();

            var result = await testObject.DeleteAsync(fixture.PersistedTodoItems.First().Id);

            fixture.AssertResultIsNoContentResult(result);
        }

        [Test]
        public async Task DeleteAsync_WithPersistedTodoItem_ShouldInvokeRepositoryCorrectly()
        {
            var testObject = fixture.CreateTestObject();

            await testObject.DeleteAsync(fixture.PersistedTodoItems.First().Id);

            fixture.AssertRepositoryRemoveInvoked();
        }

        [Test]
        public async Task DeleteAsync_WithNotPersistedTodoItem_ShouldReturnNotFoundResult()
        {
            var testObject = fixture.CreateTestObject();

            var result = await testObject.DeleteAsync(Guid.NewGuid().ToString());

            fixture.AssertResultIsNotFoundResult(result);
        }

        [Test]
        public async Task UpdateAsync_WithPersistedTodoItem_ShouldReturnNoContentResult()
        {
            var testObject = fixture.CreateTestObject();

            var persistedTodoItem = fixture.PersistedTodoItems.First();
            var result = await testObject.UpdateAsync(persistedTodoItem.Id, persistedTodoItem);

            fixture.AssertResultIsOkResult(result);
        }

        [Test]
        public async Task UpdateAsync_WithPersistedTodoItem_ShouldInvokeRepositoryCorrectly()
        {
            var testObject = fixture.CreateTestObject();

            var persistedTodoItem = fixture.PersistedTodoItems.First();
            var result = await testObject.UpdateAsync(persistedTodoItem.Id, persistedTodoItem);

            fixture.AssertRepositoryUpdateInvoked();
        }

        [Test]
        public async Task UpdateAsync_WithNotPersistedTodoItem_ShouldReturnNotFoundResult()
        {
            var testObject = fixture.CreateTestObject();

            var notPersistedTodoItem = TodoItemBuilder.New().Build();
            var result = await testObject.UpdateAsync(notPersistedTodoItem.Id, notPersistedTodoItem);

            fixture.AssertResultIsNotFoundResult(result);
        }
    }
}
