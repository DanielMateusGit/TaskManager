using FluentValidation;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Validators;

public class CompleteTaskItemCommandValidator : AbstractValidator<CompleteTaskItemCommand>
{
    public CompleteTaskItemCommandValidator()
    {
        RuleFor(cmd => cmd.TaskId).NotEqual(Guid.Empty);
    }
}