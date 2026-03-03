using TaskManager.Domain.Events;

namespace TaskManager.Domain.Tests.Events;
using Xunit;
using FluentAssertions;

public class TaskCreatedEventTest
{
    [Fact]
    public void Constructor_WhenCalled_GeneratesGuid()
    {
        Guid Id =  Guid.NewGuid();
        TaskCreatedEvent t = new TaskCreatedEvent(Id, "task t");
        t.Id.Should().NotBeEmpty();
        t.Id.Should().Be(Id);
    }
    
    [Fact]
    public void Constructor_WhenCalled_GeneratesTitle()
    {
        Guid Id =  Guid.NewGuid();
        string title = "task t";
        TaskCreatedEvent t = new TaskCreatedEvent(Id, title);
        t.Title.Should().Be(title);        
    }
    
    [Fact]
    public void Constructor_WhenCalled_GeneratesOccuredOn()
    {
        Guid Id =  Guid.NewGuid();
        TaskCreatedEvent t = new TaskCreatedEvent(Id, "task t");
        t.OccurredOn.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));        
    }
}