namespace PGSTwitter.UnitTests
{
    using System.Threading.Tasks;
    using FluentAssertions;
    using NSubstitute;
    using Repositories.Interfaces;
    using Repositories.Models;
    using Services.Implementations;
    using Services.Interfaces;
    using Xunit;

    public class TodoItemsServiceTests
    {

        [Fact]
        public async Task GetTodoItem_When_TodoItemDoesNotExist_Then_ReturnNull()
        {
            // Arrange
            var mapperMock = Substitute.For<ITodoItemsMapper>();
            var repositoryMock = Substitute.For<ITodoItemsRepository>();

            var todoItemId = 1;

            repositoryMock.GetTodoItem(todoItemId).Returns(default(TodoItem));

            var cut = new TodoItemsService(repositoryMock, mapperMock);

            // Act
            var result = await cut.GetTodoItem(todoItemId);

            // Assert
#pragma warning disable CS4014
            repositoryMock.Received().GetTodoItem(Arg.Any<long>());
#pragma warning restore CS4014
            mapperMock.DidNotReceive().ItemToDTO(Arg.Any<TodoItem>());
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetTodoItem_When_TodoItemExists_Then_CallMapperWithItemReturnedFromRepository()
        {
            // Arrange
            var mapperMock = Substitute.For<ITodoItemsMapper>();
            var repositoryMock = Substitute.For<ITodoItemsRepository>();

            var todoItemId = 1;
            var todoItem = new TodoItem()
            {
                Id = todoItemId,
                IsComplete = true,
                Name = "TestName"
            };

            repositoryMock.GetTodoItem(todoItemId).Returns(todoItem);

            var cut = new TodoItemsService(repositoryMock, mapperMock);

            // Act
            await cut.GetTodoItem(todoItemId);

            // Assert
#pragma warning disable CS4014
            repositoryMock.Received().GetTodoItem(Arg.Is<long>(todoItemId));
#pragma warning restore CS4014
            mapperMock.Received().ItemToDTO(Arg.Is(todoItem));
        }
    }
}
