using FluentValidation;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Validators;

public class DeleteTaskItemCommandValidator : AbstractValidator<DeleteTaskItemCommand>
{
    public DeleteTaskItemCommandValidator()
    {
        RuleFor(cmd => cmd.TaskItemId).NotEqual(Guid.Empty);
    }
}