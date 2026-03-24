using MediatR;

namespace TaskManager.Application.Commands;

public record DeleteTaskItemCommand(Guid TaskItemId) : IRequest<Guid>;