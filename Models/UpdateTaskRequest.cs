namespace TaskManager.Api.Models;

public record UpdateTaskRequest
(
    string Title, 
    string? Description = null,
    bool? IsCompleted = null
    );