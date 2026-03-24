using MediatR;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Queries;

public record GetTaskByIdQuery(Guid TaskId) : IRequest<TaskItemDto?>;