using TaskManager.Domain.Events;

namespace TaskManager.Domain.Tests.Events;
using Xunit;
using FluentAssertions;

public class TaskCompletedEventTest
{
    [Fact]
    public void Constructor_WhenCalled_GeneratesGuid()
    {
        Guid id =  Guid.NewGuid();
        DateTime completedAt =  DateTime.UtcNow;
        TaskCompletedEvent t = new TaskCompletedEvent(id, completedAt);
        t.Id.Should().NotBeEmpty();
        t.Id.Should().Be(id);
    }
    
    [Fact]
    public void Constructor_WhenCalled_GeneratesCompletedAt()
    {
        Guid id =  Guid.NewGuid();
        DateTime completedAt =  DateTime.UtcNow;
        TaskCompletedEvent t = new TaskCompletedEvent(id, completedAt);
        t.CompletedAt.Should().Be(completedAt);        
    }
    
    [Fact]
    public void Constructor_WhenCalled_GeneratesOccuredOn()
    {
        Guid id =  Guid.NewGuid();
        DateTime completedAt =  DateTime.UtcNow;
        TaskCompletedEvent t = new TaskCompletedEvent(id, completedAt);
        t.OccurredOn.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));        
    }
}