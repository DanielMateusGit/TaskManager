using FluentValidation;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Validators;

public class CreateTaskItemCommandValidator : AbstractValidator<CreateTaskItemCommand>
{
    public CreateTaskItemCommandValidator()
    {
        RuleFor(cmd => cmd.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(cmd => cmd.Title).MaximumLength(200);
    }
}