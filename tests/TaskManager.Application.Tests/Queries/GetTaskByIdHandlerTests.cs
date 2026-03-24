using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using TaskManager.Application.DTOs;
using TaskManager.Application.Queries;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Application.Tests.Queries;

public class GetTaskByIdHandlerTests
{
    [Fact]
    public async Task Handle_ValidQuery_ShouldGetTaskById()
    {
        // Arrange Repo
        TaskItem taskItem = new TaskItem("test", Priority.High);
        ITaskRepository repo = Substitute.For<ITaskRepository>();
        repo.GetById(taskItem.Id, Arg.Any<CancellationToken>()).Returns(taskItem); 
        CancellationToken ct = new CancellationTokenSource().Token;
        
        // Arrange Query & Query handler
        GetTaskByIdQuery q = new GetTaskByIdQuery(taskItem.Id);
        GetTaskByIdHandler h = new GetTaskByIdHandler(repo);
        
        // Act
         TaskItemDto? r = await h.Handle(q, ct);
        
        // Assert
        await repo.Received(1).GetById(Arg.Any<Guid>(), Arg.Any<CancellationToken>());

        r.Should().NotBeNull();
        r.Id.Should().Be(taskItem.Id);
        r.Title.Should().Be(taskItem.Title);
        r.CreatedAt.Should().Be(taskItem.CreatedAt);
        r.Priority.Should().Be(taskItem.Priority.Name);
    }
    
    [Fact]
    public async Task Handle_TaskNotFound_ShouldReturnNull()
    {
        // Arrange Repo
        Guid newId = new Guid();
        ITaskRepository repo = Substitute.For<ITaskRepository>();
        repo.GetById(newId, Arg.Any<CancellationToken>()).ReturnsNull(); 
        CancellationToken ct = new CancellationTokenSource().Token;
        
        // Arrange Query & Query handler
        GetTaskByIdQuery q = new GetTaskByIdQuery(newId);
        GetTaskByIdHandler h = new GetTaskByIdHandler(repo);
        
        // Act
        TaskItemDto? r = await h.Handle(q, ct);
        
        // Assert
        await repo.Received(1).GetById(Arg.Any<Guid>(), Arg.Any<CancellationToken>());

        r.Should().BeNull();
    }
}