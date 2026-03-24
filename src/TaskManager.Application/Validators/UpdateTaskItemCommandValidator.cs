using FluentValidation;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Validators;

public class UpdateTaskItemCommandValidator : AbstractValidator<UpdateTaskItemCommand>
{
    public UpdateTaskItemCommandValidator()
    {
        RuleFor(cmd => cmd.TaskItemId).NotEmpty();
        RuleFor(cmd => cmd.UpdatedTitle).NotEmpty().MaximumLength(200);
    }
}