using NSubstitute;
using TaskManager.Application.Commands;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Application.Tests.Commands;

public class DeleteTaskItemCommandHandlerTests
{
    [Fact]                                                                                                                                                                                                
    public async Task Handle_ValidCommand_ShouldAddTaskAndSaveChanges()                                                                                                                                   
    {                                                                                                                                                                                                     
        // Arrange - crea mock, command, handler
        ITaskRepository repo = Substitute.For<ITaskRepository>();
        IUnitOfWork uow = Substitute.For<IUnitOfWork>();

        TaskItem taskItem = new TaskItem("test", Priority.High);
        repo.GetById(taskItem.Id, Arg.Any<CancellationToken>()).Returns(taskItem);

        CancellationToken ct = new CancellationTokenSource().Token;
        DeleteTaskItemCommand c = new DeleteTaskItemCommand(taskItem.Id);
        DeleteTaskItemHandler h = new DeleteTaskItemHandler(repo, uow);
        
        // Act - chiama handler.Handle(...)            
        await h.Handle(c, ct);

        // Assert - verifica che repository.AddAsync e unitOfWork.SaveChangesAsync siano stati chiamati
        await repo.Received(1).Remove(Arg.Any<TaskItem>(), Arg.Any<CancellationToken>());                                                                                                         
        await uow.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());    
    }         
}