using MediatR;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Application.Commands;

public record UpdateTaskItemCommand(Guid TaskItemId, string UpdatedTitle, Priority Priority) : IRequest<Guid>;