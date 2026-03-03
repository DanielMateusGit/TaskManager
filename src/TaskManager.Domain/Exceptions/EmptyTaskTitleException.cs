namespace TaskManager.Domain.Exceptions;

public class EmptyTaskTitleException : ArgumentException
{
    public EmptyTaskTitleException() : base("Task title cannot be empty") {}
}