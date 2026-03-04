using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(Guid id, CancellationToken ct = default);                                                                                                    
    Task<IEnumerable<Project>> GetAllAsync(CancellationToken ct = default);                                                                                                  
    Task AddAsync(Project project, CancellationToken ct = default);                                                                                                          
    void Remove(Project project);  
}