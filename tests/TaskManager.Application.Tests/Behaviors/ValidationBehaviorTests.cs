using FluentValidation;
using MediatR;
using TaskManager.Application.Behaviors;
using TaskManager.Application.Commands;
using TaskManager.Application.Validators;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Application.Tests.Behaviors;

public class ValidationBehaviorTests
{
    [Fact]
    public async Task Handle_WhenValidatorFails_ShouldThrowValidationException()
    {
        // Arrange
        string title = String.Empty;
        Priority priority = Priority.High;
        CreateTaskItemCommand cmd = new CreateTaskItemCommand(title, priority);
        CreateTaskItemCommandValidator validator = new CreateTaskItemCommandValidator();
        var behavior = new ValidationBehavior<CreateTaskItemCommand, Guid>( [validator] );
        
        // Act
        RequestHandlerDelegate<Guid> next = (ct) => Task.FromResult(Guid.NewGuid());       
        
        // Act & Assert                                                                                                                                                              
        await Assert.ThrowsAsync<ValidationException>(                                                                                                                               
            () => behavior.Handle(cmd, next, CancellationToken.None)                                                                                                                 
        );    
    }

    [Fact]
    public async Task Handle_WhenValidationPasses_ShouldCallNext()
    {
        // Arrange
        string title = "title";
        Priority priority = Priority.High;
        Guid expectedId = Guid.NewGuid();                                                                                                                                             
        CreateTaskItemCommand cmd = new CreateTaskItemCommand(title, priority);
        CreateTaskItemCommandValidator validator = new CreateTaskItemCommandValidator();
        var behavior = new ValidationBehavior<CreateTaskItemCommand, Guid>( [validator] );
        
        // Act
        RequestHandlerDelegate<Guid> next = (ct) => Task.FromResult(expectedId);       
        var result = await behavior.Handle(cmd, next, CancellationToken.None);

        Assert.Equal(expectedId, result);  // se matcha → next è stato chiamato     
    }
}