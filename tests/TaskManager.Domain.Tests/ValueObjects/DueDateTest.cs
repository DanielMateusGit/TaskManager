using FluentAssertions;
using TaskManager.Domain.Exceptions;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Domain.Tests.ValueObjects;

public class DueDateTest
{
    [Fact]                                                                                                                                         
    public void FromDateTime_WithFutureDate_CreatesDueDate()                                                                                       
    {                                                                                                                                              
        // Arrange                                                                                                                                 
        var futureDate = DateTime.UtcNow.AddDays(7);                                                                                               
                                                                                                                                                 
        // Act                                                                                                                                     
        var dueDate = DueDate.FromDateTime(futureDate);                                                                                            
                                                                                                                                                 
        // Assert                                                                                                                                  
        dueDate.Value.Should().Be(futureDate);                                                                                                     
    }    
    
    [Fact]                                                                                                                                         
    public void FromDateTime_WithPastDate_ThrowsArgumentException()                                                                                
    {                                                                                                                                              
        // Arrange                                                                                                                                 
        var pastDate = DateTime.UtcNow.AddDays(-1);                                                                                                
                                                                                                                                                 
        // Act                                                                                                                                     
        Action act = () => DueDate.FromDateTime(pastDate);                                                                                         
                                                                                                                                                 
        // Assert                                                                                                                                  
        act.Should().Throw<PastDueDateException>();                                                                                                   
    }                                         
    
    [Fact]                                                                                                                                         
    public void IsOverdue_WhenDatePassed_ReturnsTrue()                                                                                             
    {                                                                                                                                              
        // Arrange - data 1 millisecondo nel futuro                                                                                                
        var nearFuture = DateTime.UtcNow.AddMilliseconds(100);                                                                                     
        var dueDate = DueDate.FromDateTime(nearFuture);                                                                                            
                                                                                                                                                 
        // Act - aspettiamo che passi                                                                                                              
        Thread.Sleep(150);                                                                                                                         
                                                                                                                                                 
        // Assert                                                                                                                                  
        dueDate.IsOverdue.Should().BeTrue();                                                                                                     
    }   
    
    [Fact]                                                                                                                                         
    public void IsOverdue_WhenDateFuture_ReturnsFalse()                                                                                            
    {                                                                                                                                              
        // Arrange                                                                                                                                 
        var futureDate = DateTime.UtcNow.AddDays(7);                                                                                               
        var dueDate = DueDate.FromDateTime(futureDate);                                                                                            
                                                                                                                                                 
        // Assert                                                                                                                                  
        dueDate.IsOverdue.Should().BeFalse();                                                                                                      
    }           
    
    [Fact]                                                                                                                                         
    public void DaysRemaining_ReturnsCorrectDays()                                                                                                 
    {                                                                                                                                              
        // Arrange                                                                                                                                 
        var futureDate = DateTime.UtcNow.AddDays(7);                                                                                               
        var dueDate = DueDate.FromDateTime(futureDate);                                                                                            
                                                                                                                                                 
        // Act                                                                                                                                     
        var days = dueDate.DaysRemaining();                                                                                                        
                                                                                                                                                 
        // Assert                                                                                                                                  
        days.Should().Be(7);                                                                                                                       
    }                                                                                                                                              
}