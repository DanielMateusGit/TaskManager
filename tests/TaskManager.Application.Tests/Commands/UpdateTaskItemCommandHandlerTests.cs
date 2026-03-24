using NSubstitute;
using TaskManager.Application.Commands;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Application.Tests.Commands;

public class UpdateTaskItemCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidCommand_ShouldUpdateTaskAndSaveChanges()
    {
        // Arrange
        IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();
        ITaskRepository repository = Substitute.For<ITaskRepository>();

        TaskItem taskItem = new TaskItem("unmodified", Priority.High);
        repository.GetById(taskItem.Id, Arg.Any<CancellationToken>()).Returns(taskItem);

        UpdateTaskItemCommand cmd = new UpdateTaskItemCommand(taskItem.Id, "updated title", Priority.Low);
        UpdateTaskItemCommandHandler handler = new UpdateTaskItemCommandHandler(repository, unitOfWork);

        // Act
        await handler.Handle(cmd, CancellationToken.None);

        // Assert
        await repository.Received(1).GetById(taskItem.Id, Arg.Any<CancellationToken>());
        await repository.Received(1).Update(Arg.Any<TaskItem>());
        await unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }         
}