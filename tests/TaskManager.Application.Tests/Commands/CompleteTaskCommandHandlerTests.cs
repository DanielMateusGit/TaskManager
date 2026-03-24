using NSubstitute;
using TaskManager.Application.Commands;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Application.Tests.Commands;

public class CompleteTaskCommandHandlerTests
{
    [Fact]                                                                                                                                                                                                
    public async Task Handle_ValidCommand_ShouldAddTaskAndSaveChanges()                                                                                                                                   
    {                                                                                                                                                                                                     
        // Arrange - crea mock, command, handler
        ITaskRepository repo = Substitute.For<ITaskRepository>();
        IUnitOfWork uow = Substitute.For<IUnitOfWork>();
        TaskItem taskItem = new TaskItem("test", Priority.High);
        CompleteTaskItemHandler h = new CompleteTaskItemHandler(repo, uow);
        Guid taskItemId = taskItem.Id;
        repo.GetById(taskItemId, Arg.Any<CancellationToken>()).Returns(taskItem); 
        CompleteTaskItemCommand c = new CompleteTaskItemCommand(taskItemId);
        CancellationToken ct = new CancellationTokenSource().Token;
            
        
        // Act - chiama handler.Handle(...)            
        await h.Handle(c, ct);

        await repo.Received(1).GetById(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        await repo.Received(1).Update(Arg.Any<TaskItem>());
        await uow.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());    
    }        
    
}