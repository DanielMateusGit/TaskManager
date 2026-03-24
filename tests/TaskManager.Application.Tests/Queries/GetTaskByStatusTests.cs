using FluentAssertions;
using NSubstitute;
using TaskManager.Application.DTOs;
using TaskManager.Application.Queries;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Application.Tests.Queries;

public class GetTaskByStatusQueryTests
{
    [Fact]
    public async Task Handle_ValidQuery_ShouldReturnAllTasks()
    {
        // Arrange Repo
        IEnumerable<TaskItem> results = new List<TaskItem>()
        {
            new TaskItem("testA", Priority.High),
            new TaskItem("testB", Priority.High),
            new TaskItem("testC", Priority.High),
        };

        foreach (var task in results)
        {
            task.Complete();
        }

        ITaskRepository repository = Substitute.For<ITaskRepository>();
        repository.GetByStatus(true,Arg.Any<CancellationToken>()).Returns(results);
        
        // Arrange Query and Handler
        GetTaskByStatusQuery q = new GetTaskByStatusQuery(true);
        GetTaskByStatusHandler h = new GetTaskByStatusHandler(repository);
        
        // Act
        IEnumerable<TaskItemDto> res = await h.Handle(q, CancellationToken.None);
        
        // Assert
        res.Should().NotBeNull();
        res.Count().Should().Be(3);
        res.First().Title.Should().Be("testA");
    }
    
    [Fact]
    public async Task Handle_ValidQuery_ShouldReturnNoneTasks()
    {
        // Arrange Repo
        IEnumerable<TaskItem> results = new List<TaskItem>()
        {
        };

        ITaskRepository repository = Substitute.For<ITaskRepository>();
        repository.GetByStatus(true,Arg.Any<CancellationToken>()).Returns(results);
        
        // Arrange Query and Handler
        GetTaskByStatusQuery q = new GetTaskByStatusQuery(true);
        GetTaskByStatusHandler h = new GetTaskByStatusHandler(repository);
        
        // Act
        IEnumerable<TaskItemDto> res = await h.Handle(q, CancellationToken.None);
        
        // Assert
        res.Should().NotBeNull();
        res.Count().Should().Be(0);
    }
}