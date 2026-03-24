using MediatR;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Commands;

public class CreateTaskItemHandler : IRequestHandler<CreateTaskItemCommand, Guid>
{
    private readonly ITaskRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTaskItemHandler(ITaskRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Guid> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
    {
        TaskItem newTask = new TaskItem(request.Title, request.Priority);
        await _repository.Add(newTask, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return newTask.Id;
    }
}