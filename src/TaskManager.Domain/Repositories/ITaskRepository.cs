using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories;

public interface ITaskRepository
{
    Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken ct = default);                                                                                                   
    Task<IEnumerable<TaskItem>> GetAllAsync(CancellationToken ct = default);                                                                                                 
    Task AddAsync(TaskItem task, CancellationToken ct = default);                                                                                                            
    void Remove(TaskItem task);      
}