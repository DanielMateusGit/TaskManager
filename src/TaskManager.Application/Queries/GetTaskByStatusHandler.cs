using MediatR;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Queries;

public class GetTaskByStatusHandler : IRequestHandler<GetTaskByStatusQuery, IEnumerable<TaskItemDto>>
{
    private readonly ITaskRepository _repository;
    public GetTaskByStatusHandler(ITaskRepository repository) => _repository = repository;
    
    public async Task<IEnumerable<TaskItemDto>> Handle(GetTaskByStatusQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<TaskItem> result = await _repository.GetByStatus(request.IsCompleted, cancellationToken);
        
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