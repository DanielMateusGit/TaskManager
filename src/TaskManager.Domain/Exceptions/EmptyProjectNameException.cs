namespace TaskManager.Domain.Exceptions;

public class EmptyProjectNameException : ArgumentException
{
    public EmptyProjectNameException() : base("Project name cannot be empty") {}
}