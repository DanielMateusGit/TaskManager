namespace TaskManager.Domain.Exceptions;

public class TaskAlreadyCompletedException : InvalidOperationException
{
    public Guid TaskId { get; }

    public TaskAlreadyCompletedException(Guid taskId) : base($"Task {taskId} is already completed")         
    {
        TaskId = taskId;  
    }
}