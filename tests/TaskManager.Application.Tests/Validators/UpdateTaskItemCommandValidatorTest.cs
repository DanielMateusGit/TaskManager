using FluentValidation.TestHelper;
using TaskManager.Application.Commands;
using TaskManager.Application.Validators;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Application.Tests.Validators;

public class UpdateTaskItemCommandValidatorTest
{
    [Fact]
    public void UpdateTaskItemCommand_withEmptyId_ShouldReturnError()
    {
        // Arrange
        Guid taskItemId = Guid.Empty;
        UpdateTaskItemCommandValidator validator = new UpdateTaskItemCommandValidator();
        UpdateTaskItemCommand command = new UpdateTaskItemCommand(taskItemId, "updatedTitle", Priority.Critical);
        
        // Act
        var result = validator.TestValidate(command);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.TaskItemId);
    }
    
    [Fact]
    public void UpdateTaskItemCommand_withEmptyTitle_ShouldReturnError()
    {
        // Arrange
        Guid taskItemId = Guid.NewGuid();
        UpdateTaskItemCommandValidator validator = new UpdateTaskItemCommandValidator();
        UpdateTaskItemCommand command = new UpdateTaskItemCommand(taskItemId, "", Priority.Critical);
        
        // Act
        var result = validator.TestValidate(command);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UpdatedTitle);
    }
    
    [Fact]
    public void UpdateTaskItemCommand_withTooLongText_ShouldReturnError()
    {
        // Arrange
        Guid taskItemId = Guid.NewGuid();
        string tooLongTitle = new string('x', 201);
        
        UpdateTaskItemCommandValidator validator = new UpdateTaskItemCommandValidator();
        UpdateTaskItemCommand command = new UpdateTaskItemCommand(taskItemId, tooLongTitle, Priority.Critical);
        
        // Act
        var result = validator.TestValidate(command);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UpdatedTitle);
    }

    [Fact]
    public void UpdateTaskItemCommand_withNoErrors_ShouldReturnNoError()
    {
        // Arrange
        Guid taskItemId = Guid.NewGuid();
        string okTitle  = new string('x', 199);
        
        UpdateTaskItemCommandValidator validator = new UpdateTaskItemCommandValidator();
        UpdateTaskItemCommand command = new UpdateTaskItemCommand(taskItemId, okTitle, Priority.Critical);
        
        // Act
        var result = validator.TestValidate(command);
        
        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}