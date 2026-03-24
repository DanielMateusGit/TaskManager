using FluentValidation.TestHelper;
using TaskManager.Application.Commands;
using TaskManager.Application.Validators;

namespace TaskManager.Application.Tests.Validators;

public class DeleteTaskItemCommandValidatorTests
{
    [Fact]
    public void DeleteTask_withEmptyGuid_ShouldHaveValidationError()
    {
        // Arrange
        Guid taskId = Guid.Empty;
        DeleteTaskItemCommandValidator validator = new DeleteTaskItemCommandValidator();
        DeleteTaskItemCommand command = new DeleteTaskItemCommand(taskId);
        
        // Act
        var result = validator.TestValidate(command);
        
        // Assert
        result.ShouldHaveValidationErrorFor(r => r.TaskItemId);
    }
    
    [Fact]
    public void DeleteTask_withNotEmptyGuid_ShouldNotHaveValidationError()
    {
        // Arrange
        Guid taskId = Guid.NewGuid();
        DeleteTaskItemCommandValidator validator = new DeleteTaskItemCommandValidator();
        DeleteTaskItemCommand command = new DeleteTaskItemCommand(taskId);
        
        // Act
        var result = validator.TestValidate(command);
        
        // Assert
        result.ShouldNotHaveValidationErrorFor(r => r.TaskItemId);
    }
}