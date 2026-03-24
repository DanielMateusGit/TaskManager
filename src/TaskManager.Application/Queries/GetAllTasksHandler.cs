using MediatR;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Queries;

public class GetAllTasksHandler : IRequestHandler<GetAllTasksQuery,  IEnumerable<TaskItemDto>>
{
    private readonly ITaskRepository _repository;

    public GetAllTasksHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskItemDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<TaskItem> result = await _repository.GetAll(cancellationToken);
        
        return result.Select(t => new TaskItemDto(
            t.Id,
            t.Title, 
            t.Priority.Name, 
            t.IsCompleted, 
            t.CreatedAt, 
            t.CompletedAt)
        );
    }
}