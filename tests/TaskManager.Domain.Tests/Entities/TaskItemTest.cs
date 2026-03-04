using TaskManager.Domain.Entities;
using TaskManager.Domain.ValueObjects;
using FluentAssertions;
using TaskManager.Domain.Exceptions;
using Xunit;

namespace TaskManager.Domain.Tests.Entities;

public class TaskItemTest
{
    [Fact]
    public void TaskItem_WhenCreated_HasCorrectTitle()
    {
        // Arrange
        string title = "Buy some milk";
        
        // Act
        TaskItem task = new TaskItem(title, Priority.Low);
        
        // Assert
        task.Title.Should().Be(title);
    }

    [Fact]                                                                                                                                         
    public void Constructor_WithEmptyTitle_ThrowsArgumentException()                                                                               
    {                                                                                                                                              
        // Arrange                                                                                                                                 
        var emptyTitle = "";                                                                                                                       
                                                                                                                                                 
        // Act                                                                                                                                     
        Action act = () => new TaskItem(emptyTitle, Priority.Low);                                                                                               
                                                                                                                                                 
        // Assert                                                                                                                                  
        act.Should().Throw<ArgumentException>();                                                                                                   
    }       

    [Fact]
    public void TaskItem_WhenCreated_GeneratesUniqueId()
    {
        // Arrange & Act
        TaskItem task1 = new TaskItem("Buy some milk", Priority.Low);
        TaskItem task2 = new TaskItem("Buy some milk", Priority.Low);
        
        // Assert
        task1.Id.Should().NotBeEmpty();
        task2.Id.Should().NotBeEmpty();
        task1.Id.Should().NotBe(task2.Id);
    }

    [Fact]                                                                                                                                         
    public void Constructor_WithPriority_SetsPriority()                                                                                            
    {                                                                                                                                              
        // Arrange                                                                                                                                 
        var title = "Buy milk";                                                                                                                    
        var priority = Priority.High;                                                                                                              
                                                                                                                                                 
        // Act                                                                                                                                     
        var task = new TaskItem(title, priority);                                                                                                  
                                                                                                                                                 
        // Assert                                                                                                                                  
        task.Priority.Should().Be(Priority.High);                                                                                                  
    }

    [Fact]
    public void Complete_WhenCalled_SetsIsCompletedTrue()
    {
        // Arrange 
        TaskItem task = new TaskItem("Buy milk", Priority.Low);
        
        // Act 
        task.Complete();
        
        // Assert
        task.IsCompleted.Should().BeTrue();
        task.DomainEvents.Should().HaveCountGreaterThan(0);
    }
                                                                                                                                                     
    [Fact]                                                                                                                                         
    public void Complete_WhenCalled_SetsCompletedAt()                                                                                              
    {                                                                                                                                              
      // Arrange                                                                                                                                 
      var task = new TaskItem("Buy milk", Priority.Low);                                                                                         
                                                                                                                                                 
      // Act                                                                                                                                     
      task.Complete();                                                                                                                           
                                                                                                                                                 
      // Assert                                                                                                                                  
      task.CompletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));   
      task.DomainEvents.Should().HaveCountGreaterThan(0);
    }                                                
    
    [Fact]                                                                                                                                         
    public void Complete_WhenAlreadyCompleted_ThrowsInvalidOperationException()                                                                    
    {                                                                                                                                              
        // Arrange                                                                                                                                 
        var task = new TaskItem("Buy milk", Priority.Low);                                                                                         
        task.Complete();                                                                                                                           
                                                                                                                                                 
        // Act                                                                                                                                     
        Action act = () => task.Complete();                                                                                                        
                                                                                                                                                 
        // Assert                                                                                                                                  
        act.Should().Throw<InvalidOperationException>();        
    }
    
    [Fact]                                                                                                                                         
    public void Constructor_WhenCalled_SetsCreatedAt()                                                                                             
    {                                                                                                                                              
        // Act                                                                                                                                     
        var task = new TaskItem("Buy milk", Priority.Low);                                                                                         
                                                                                                                                                 
        // Assert                                                                                                                                  
        task.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));                                                               
    } 
    
    [Fact]                                                                                                                                         
  public void UpdateTitle_WithValidTitle_UpdatesTitle()                                                                                          
  {                                                                                                                                              
      // Arrange                                                                                                                                 
      var task = new TaskItem("Old title", Priority.Low);                                                                                        
                                                                                                                                                 
      // Act                                                                                                                                     
      task.UpdateTitle("New title");                                                                                                             
                                                                                                                                                 
      // Assert                                                                                                                                  
      task.Title.Should().Be("New title");                                                                                                       
  }                                                                                                                                              
                                                                                                                                                 
  [Fact]                                                                                                                                         
  public void UpdateTitle_WithEmptyTitle_ThrowsEmptyTaskTitleException()                                                                         
  {                                                                                                                                              
      // Arrange                                                                                                                                 
      var task = new TaskItem("Buy milk", Priority.Low);                                                                                         
                                                                                                                                                 
      // Act                                                                                                                                     
      Action act = () => task.UpdateTitle("");                                                                                                   
                                                                                                                                                 
      // Assert                                                                                                                                  
      act.Should().Throw<EmptyTaskTitleException>();                                                                                             
  }         
  
  [Fact]                                                                                                                                         
  public void ChangePriority_WithNewPriority_UpdatesPriority()                                                                                   
  {                                                                                                                                              
      // Arrange                                                                                                                                 
      var task = new TaskItem("Buy milk", Priority.Low);                                                                                         
                                                                                                                                                 
      // Act                                                                                                                                     
      task.ChangePriority(Priority.Critical);                                                                                                    
                                                                                                                                                 
      // Assert                                                                                                                                  
      task.Priority.Should().Be(Priority.Critical);                                                                                              
  }

  [Fact]
  public void CreateTaskItem_WithValidTask_AddDomainEvent()
  {
      // Assert + Act
      TaskItem task = new TaskItem("Buy milk", Priority.Low);
      task.DomainEvents.Count().Should().Be(1);
  }
}