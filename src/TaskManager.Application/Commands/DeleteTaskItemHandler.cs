using MediatR;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Commands;

public class DeleteTaskItemHandler : IRequestHandler<DeleteTaskItemCommand, Guid>
{

    private readonly ITaskRepository _repository;
    private readonly IUnitOfWork _uow;
    public DeleteTaskItemHandler(ITaskRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }
    public async Task<Guid> Handle(DeleteTaskItemCommand request, CancellationToken cancellationToken)
    {
        TaskItem t = await _repository.GetById(request.TaskItemId, cancellationToken) ?? throw new InvalidOperationException();
        await _repository.Remove(t, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        
        return t.Id;
    }
}