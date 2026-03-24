using MediatR;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Commands;

public class CompleteTaskItemHandler : IRequestHandler<CompleteTaskItemCommand, Guid>
{
    private readonly ITaskRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    public CompleteTaskItemHandler(ITaskRepository repository, IUnitOfWork unitOfWork)
    {
        this._repository = repository;
        this._unitOfWork = unitOfWork;
    }
    public async Task<Guid> Handle(CompleteTaskItemCommand request, CancellationToken cancellationToken)
    {
        TaskItem t = await _repository.GetById(request.TaskId, cancellationToken) ?? throw new InvalidOperationException();
        t.Complete();
        await _repository.Update(t);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return t.Id;
    }
}