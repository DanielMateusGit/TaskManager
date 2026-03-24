using MediatR;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Queries;

public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, TaskItemDto?>
{
    private readonly ITaskRepository _repository;

    public GetTaskByIdHandler(ITaskRepository repository) => _repository = repository;

    public async Task<TaskItemDto?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        TaskItem? task = await _repository.GetById(request.TaskId, cancellationToken);
        if (task == null) return null;

        return new TaskItemDto
        (
            task.Id,
            task.Title,
            task.Priority.Name,
            task.IsCompleted,
            task.CreatedAt,
            task.CompletedAt
        );
    }
}