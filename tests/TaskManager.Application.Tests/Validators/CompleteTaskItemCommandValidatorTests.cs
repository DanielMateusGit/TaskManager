using FluentValidation.TestHelper;
using TaskManager.Application.Commands;
using TaskManager.Application.Validators;

namespace TaskManager.Application.Tests.Validators;

public class CompleteTaskItemCommandValidatorTests
{
    [Fact]
    public void CompleteTask_withEmptyGuid_ShouldHaveValidationError()
    {
        // Arrange
        Guid taskId = Guid.Empty;
        CompleteTaskItemCommandValidator validator = new CompleteTaskItemCommandValidator();
        CompleteTaskItemCommand command = new CompleteTaskItemCommand(taskId);
        
        // Act
        var result = validator.TestValidate(command);
        
        // Assert
        result.ShouldHaveValidationErrorFor(r => r.TaskId);
    }
    
    [Fact]
    public void CompleteTask_withNotEmptyGuid_ShouldNotHaveValidationError()
    {
        // Arrange
        Guid taskId = Guid.NewGuid();
        CompleteTaskItemCommandValidator validator = new CompleteTaskItemCommandValidator();
        CompleteTaskItemCommand command = new CompleteTaskItemCommand(taskId);
        
        // Act
        var result = validator.TestValidate(command);
        
        // Assert
        result.ShouldNotHaveValidationErrorFor(r => r.TaskId);
    }
}