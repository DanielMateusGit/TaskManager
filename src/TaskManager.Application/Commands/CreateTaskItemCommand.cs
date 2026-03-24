using MediatR;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Application.Commands;

public record CreateTaskItemCommand(string Title, Priority Priority) : IRequest<Guid>;