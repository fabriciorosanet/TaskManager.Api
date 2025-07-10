namespace TaskManager.Api.Services;
using TaskManager.Api.Data;
using TaskManager.Api.Models;
using Microsoft.EntityFrameworkCore;

public class TaskService
{
    private readonly TaskContext _context;

    public TaskService(TaskContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskItem>> GetAllTasksAsync(int userId, string? status = null, string? search = null)
    {
        var query = _context.Tasks
            .Where(t => t.UserId == userId) 
            .AsQueryable();

        // Filtrar por status se fornecido
        if (!string.IsNullOrEmpty(status))
        {
            var isCompleted = status.ToLower() == "completed";
            query = query.Where(t => t.IsCompleted == isCompleted);
        }

        // Filtrar por pesquisa se fornecido
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(t => t.Title.Contains(search) || 
                                   (t.Description != null && t.Description.Contains(search)));
        }

        return await query.OrderByDescending(t => t.CreatedAt).ToListAsync();
    }

    public async Task<TaskItem?> GetTaskByIdAsync(int id, int userId)
    {
        return await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
    }

    public async Task<TaskItem> CreateTaskAsync(CreateTaskRequest request, int userId)
    {
        var task = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
            UserId = userId
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<TaskItem?> UpdateTaskAsync(int id, UpdateTaskRequest request, int userId)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        
        if (task == null) return null;

        // Atualizar apenas os campos fornecidos
        if (!string.IsNullOrEmpty(request.Title))
            task.Title = request.Title;

        if (request.Description != null)
            task.Description = request.Description;

        if (request.IsCompleted.HasValue)
        {
            task.IsCompleted = request.IsCompleted.Value;
            task.CompletedAt = request.IsCompleted.Value ? DateTime.UtcNow : null;
        }

        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<bool> DeleteTaskAsync(int id, int userId)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        
        if (task == null) return false;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
}