using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories;

public interface ITagRepository
{
    Task<Tag?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Tag>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(Tag tag, CancellationToken ct = default);
    Task<Tag?> GetByNameAsync(string tagName, CancellationToken ct = default);
    void Remove(Tag tag);
}