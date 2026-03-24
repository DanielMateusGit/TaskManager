using MediatR;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Queries;

public record GetTaskByStatusQuery(bool IsCompleted) : IRequest<IEnumerable<TaskItemDto>>;