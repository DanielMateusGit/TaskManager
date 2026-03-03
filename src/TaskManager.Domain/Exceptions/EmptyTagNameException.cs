namespace TaskManager.Domain.Exceptions;

public class EmptyTagNameException : ArgumentException
{
    public EmptyTagNameException() : base("Tag name cannot be empty") {}
}