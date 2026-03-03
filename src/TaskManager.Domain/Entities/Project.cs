using TaskManager.Domain.Exceptions;

namespace TaskManager.Domain.Entities;

public class Project
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; }
    
    public Project(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new EmptyProjectNameException();
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.CreatedAt = DateTime.UtcNow;
    }
    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName)) throw new EmptyProjectNameException();
        this.Name = newName;
    }

    public void UpdateDescription(string newDescription)
    {
        this.Description = newDescription;
    }
}