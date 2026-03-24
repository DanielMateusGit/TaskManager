using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories;

public interface ITaskRepository
{
    Task<TaskItem?> GetById(Guid id, CancellationToken ct = default);                                                                                                   
    Task<IEnumerable<TaskItem>> GetAll(CancellationToken ct = default);                                                                                                 
    Task<IEnumerable<TaskItem>> GetByStatus(bool isCompleted, CancellationToken ct = default);
    Task Add(TaskItem task, CancellationToken ct = default);
    Task Update(TaskItem task);
    Task Remove(TaskItem task, CancellationToken any);      
}