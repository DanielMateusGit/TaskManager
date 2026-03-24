using MediatR;

namespace TaskManager.Application.Commands;

public record CompleteTaskItemCommand(Guid TaskId) : IRequest<Guid>;
