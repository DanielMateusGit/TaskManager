using TaskManager.Domain.Exceptions;

namespace TaskManager.Domain.Entities;

public class Tag
{
    public Guid Id {get;} 
    public string Name { get; private set; }
    public string? Color { get; private set; }

    public Tag(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new EmptyTagNameException();
        this.Id = Guid.NewGuid();
        this.Name = name;
    }
    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new EmptyTagNameException();
        this.Name = name;
    }
    public void ChangeColor(string color)
    {
        this.Color = color;
    }
}