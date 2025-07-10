using System.Security.Claims;

namespace TaskManager.Api.Endpoints;
using TaskManager.Api.Models;
using TaskManager.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;

public static class TasksEndpoints
{
    public static void MapTasksEndpoints(this WebApplication app)
    {
        var tasks = app.MapGroup("/api/tasks")
            .WithTags("Tasks")
            .WithOpenApi()
            .RequireAuthorization(); // Requer autenticação para acessar todos os endpoints

        // GET /api/tasks - Listar todas as tarefas
        tasks.MapGet("/", GetAllTasks)
            .WithName("GetAllTasks")
            .WithSummary("Listar todas as tarefas")
            .WithDescription("Retorna uma lista de todas as tarefas. Permite filtrar por status e pesquisar por título/descrição.")
            .Produces<IEnumerable<TaskItem>>(StatusCodes.Status200OK);

        // GET /api/tasks/{id} - Obter tarefa por ID
        tasks.MapGet("/{id:int}", GetTaskById)
            .WithName("GetTaskById")
            .WithSummary("Obter tarefa por ID")
            .WithDescription("Retorna uma tarefa específica pelo seu ID.")
            .Produces<TaskItem>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        // POST /api/tasks - Criar nova tarefa
        tasks.MapPost("/", CreateTask)
            .WithName("CreateTask")
            .WithSummary("Criar nova tarefa")
            .WithDescription("Cria uma nova tarefa com título e descrição opcional.")
            .Produces<TaskItem>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        // PUT /api/tasks/{id} - Atualizar tarefa
        tasks.MapPut("/{id:int}", UpdateTask)
            .WithName("UpdateTask")
            .WithSummary("Atualizar tarefa")
            .WithDescription("Atualiza uma tarefa existente. Todos os campos são opcionais.")
            .Produces<TaskItem>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest);

        // DELETE /api/tasks/{id} - Deletar tarefa
        tasks.MapDelete("/{id:int}", DeleteTask)
            .WithName("DeleteTask")
            .WithSummary("Deletar tarefa")
            .WithDescription("Remove uma tarefa permanentemente.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> GetAllTasks(
        ClaimsPrincipal user,
        TaskService taskService,
        [FromQuery] string? status = null,
        [FromQuery] string? search = null)
    {
        var userId = GetUserId(user);
        var tasks = await taskService.GetAllTasksAsync(userId ,status, search);
        return Results.Ok(tasks);
    }

    private static async Task<IResult> GetTaskById(int id, ClaimsPrincipal user ,TaskService taskService)
    {
        var userId = GetUserId(user);
        var task = await taskService.GetTaskByIdAsync(id, userId);
        return task is not null ? Results.Ok(task) : Results.NotFound();
    }

    private static async Task<IResult> CreateTask(
        CreateTaskRequest request,
        ClaimsPrincipal user,
        TaskService taskService)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return Results.BadRequest("O título da tarefa é obrigatório.");
        }
        
        var userId = GetUserId(user);
        var task = await taskService.CreateTaskAsync(request, userId);
        return Results.Created($"/api/tasks/{task.Id}", task);
    }

    private static async Task<IResult> UpdateTask(
        int id,
        UpdateTaskRequest request,
        ClaimsPrincipal user,
        TaskService taskService)
    {
        var userId = GetUserId(user);
        var task = await taskService.UpdateTaskAsync(id, request, userId);
        return task is not null ? Results.Ok(task) : Results.NotFound();
    }

    private static async Task<IResult> DeleteTask(
        int id,
        ClaimsPrincipal user ,
        TaskService taskService)
    {
        var userId = GetUserId(user);
        var deleted = await taskService.DeleteTaskAsync(id, userId);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
    
    private static int GetUserId(ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            throw new UnauthorizedAccessException("Usuário não autenticado.");
        }
        return userId;
    }
}