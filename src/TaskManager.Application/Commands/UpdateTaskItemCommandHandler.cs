using MediatR;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Commands;

public class UpdateTaskItemCommandHandler : IRequestHandler<UpdateTaskItemCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaskRepository _repository;

    public UpdateTaskItemCommandHandler(ITaskRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Guid> Handle(UpdateTaskItemCommand request, CancellationToken cancellationToken)
    {
        Guid taskId = request.TaskItemId;
        TaskItem t =  await _repository.GetById(taskId, cancellationToken) ?? throw new InvalidOperationException();
        t.UpdateTitle(request.UpdatedTitle);
        t.ChangePriority(request.Priority);
        await _repository.Update(t);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return taskId;
    }
}