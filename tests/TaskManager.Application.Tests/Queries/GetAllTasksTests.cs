using FluentAssertions;
using NSubstitute;
using TaskManager.Application.DTOs;
using TaskManager.Application.Queries;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Application.Tests.Queries;

public class GetAllTasksTests
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

        ITaskRepository repository = Substitute.For<ITaskRepository>();
        repository.GetAll(Arg.Any<CancellationToken>()).Returns(results);
        
        // Arrange Query and Handler
        GetAllTasksQuery q = new GetAllTasksQuery();
        GetAllTasksHandler h = new GetAllTasksHandler(repository);
        
        // Act
        IEnumerable<TaskItemDto> res = await h.Handle(q, CancellationToken.None);
        
        // Assert
        res.Should().NotBeNull();
        res.Count().Should().Be(3);
        res.First().Title.Should().Be("testA");
    }
    
    [Fact]
    public async Task Handle_ValidQuery_ShouldReturnAllTasksEmptyList()
    {
        // Arrange Repo
        IEnumerable<TaskItem> results = new List<TaskItem>() { };

        ITaskRepository repository = Substitute.For<ITaskRepository>();
        repository.GetAll(Arg.Any<CancellationToken>()).Returns(results);
        
        // Arrange Query and Handler
        GetAllTasksQuery q = new GetAllTasksQuery();
        GetAllTasksHandler h = new GetAllTasksHandler(repository);
        
        // Act
        IEnumerable<TaskItemDto> res = await h.Handle(q, CancellationToken.None);
        
        // Assert
        res.Should().NotBeNull();
        res.Count().Should().Be(0);
    }
}