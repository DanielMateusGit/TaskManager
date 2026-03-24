namespace TaskManager.Application.DTOs;

public record TaskItemDto(Guid Id, string Title, string Priority, bool IsCompleted, DateTime CreatedAt, DateTime? CompletedAt );