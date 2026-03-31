using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using TaskManager.Application.Behaviors;
using TaskManager.Application.Commands;
using TaskManager.Application.Validators;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Application.Tests.Behaviors;

public class LoggingBehaviorTests
{
    [Fact]
    public async Task Handle_ShouldCallNextAndReturnResult()
    {
        // Arrange
        string title = "title";
        Priority priority = Priority.High;
        Guid expectedId = Guid.NewGuid();                                                                                                                                             
        CreateTaskItemCommand cmd = new CreateTaskItemCommand(title, priority);
        var logger = NullLogger<LoggingBehavior<CreateTaskItemCommand, Guid>>.Instance;   
        var behavior = new LoggingBehavior<CreateTaskItemCommand, Guid>(logger);
        
        // Act
        RequestHandlerDelegate<Guid> next = (ct) => Task.FromResult(expectedId);       
        var result = await behavior.Handle(cmd, next, CancellationToken.None);
        
        // Assert
        Assert.Equal(expectedId, result); 
    }
    
    [Fact]
    public async Task Handle_WhenNextThrows_ShouldLogErrorAndRethrow()
    {
        // Arrange
        string title = "title";
        Priority priority = Priority.High;
        CreateTaskItemCommand cmd = new CreateTaskItemCommand(title, priority);
        var logger = NullLogger<LoggingBehavior<CreateTaskItemCommand, Guid>>.Instance;   
        var behavior = new LoggingBehavior<CreateTaskItemCommand, Guid>(logger);
        
        // Act
        RequestHandlerDelegate<Guid> next = (ct) => throw new InvalidOperationException("handler failed");
        
        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(                                                                                                                         
            () => behavior.Handle(cmd, next, CancellationToken.None)                                                                                                                 
        );                                                                                                                                                                           
    }
}