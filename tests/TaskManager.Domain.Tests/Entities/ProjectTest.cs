using TaskManager.Domain.Entities;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Domain.Tests.Entities;
using Xunit;
using FluentAssertions;

public class ProjectTest
{
    [Fact]
    public void Constructor_WithValidName_SetsName()
    {
        // Arrange & Act
        Project p = new Project("This is a valid name");

        // Assert 
        p.Name.Should().Be("This is a valid name");
    }

    [Fact]
    public void Constructor_WhenCalled_GeneratesUniqueId()
    {
        // Arrange & Act
        Project p = new Project("Project name");

        // Assert
        p.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void Constructor_WhenCalled_SetsCreatedAt()
    {
        Project p = new Project("Project name");
        p.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void Constructor_WithEmptyName_ThrowsException()
    {
        string emptyName = string.Empty;
        Action act = () => new Project(emptyName);                                                                                               
        act.Should().Throw<EmptyProjectNameException>();     
    }

    [Fact]
    public void Rename_WithValidName_UpdatesName()
    {
        string newName = "New Name";
        Project p = new Project("Project name");
        p.Rename(newName);
        p.Name.Should().Be(newName);
    }

    [Fact]
    public void Rename_WithEmptyName_ThrowsException()
    {
        string emptyName = string.Empty;
        Project p = new Project("Project name");
        Action act = () => p.Rename(emptyName);                                                                                               
        act.Should().Throw<EmptyProjectNameException>();     
    }

    [Fact]
    public void UpdateDescription_SetsDescription()
    {
        string description = "New Description";
        Project p = new Project("Project name");
        p.UpdateDescription(description);
        p.Description.Should().Be(description);
    }
}