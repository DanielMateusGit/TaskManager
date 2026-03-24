using FluentValidation.TestHelper;
using TaskManager.Application.Commands;
using TaskManager.Application.Validators;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Application.Tests.Validators;

public class CreateTaskItemCommandValidatorTests
{
    [Fact]
    public void CreateTaskItemCommand_withNoTitle_ShouldReturnError()
    {
        // Arrange
        CreateTaskItemCommandValidator validator = new CreateTaskItemCommandValidator();
        CreateTaskItemCommand c = new CreateTaskItemCommand("", Priority.High);
        
        // Act
        var result = validator.TestValidate(c);
        
        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Title);
    }

    [Fact]
    public void CreateTaskItemCommand_withTooLongTitle_ShouldReturnError()
    {
        // Assert
        string wrongTitle = new string('x', 201);
        CreateTaskItemCommandValidator validator = new CreateTaskItemCommandValidator();
        CreateTaskItemCommand c = new CreateTaskItemCommand(wrongTitle, Priority.Critical);
        
        // Act
        var result = validator.TestValidate(c);
        
        // Assert
        result.ShouldHaveValidationErrorFor(command => command.Title);
    }

    [Fact]
    public void CreateTaskItemCommand_withNoProblems_ShouldNotReturnError()
    {
        CreateTaskItemCommandValidator validator = new CreateTaskItemCommandValidator();
        CreateTaskItemCommand c = new CreateTaskItemCommand("testName", Priority.High);
        var result = validator.TestValidate(c);
        result.ShouldNotHaveValidationErrorFor(command => command.Title);
    }
}