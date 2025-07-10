namespace TaskManager.Api.Endpoints;
using TaskManager.Api.Models;
using TaskManager.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var auth = app.MapGroup("/api/auth")
            .WithTags("Authentication")
            .WithOpenApi();

        auth.MapPost("/login", Login)
            .WithName("Login")
            .WithSummary("Realizar login")
            .WithDescription("Autentica um usuário e retorna um token JWT")
            .Produces<AuthResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        auth.MapPost("/register", Register)
            .WithName("Register")
            .WithSummary("Registrar novo usuário")
            .WithDescription("Cria uma nova conta de usuário")
            .Produces<AuthResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);
    }

    private static async Task<IResult> Login(
        [FromBody] LoginRequest request,
        AuthService authService)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            return Results.BadRequest("Username e password são obrigatórios");

        var result = await authService.LoginAsync(request);
        
        return result is not null 
            ? Results.Ok(result) 
            : Results.Unauthorized();
    }

    private static async Task<IResult> Register(
        [FromBody] RegisterRequest request,
        AuthService authService)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || 
            string.IsNullOrWhiteSpace(request.Email) || 
            string.IsNullOrWhiteSpace(request.Password))
            return Results.BadRequest("Todos os campos são obrigatórios");

        if (request.Password.Length < 6)
            return Results.BadRequest("A senha deve ter pelo menos 6 caracteres");

        var result = await authService.RegisterAsync(request);
        
        return result is not null 
            ? Results.Created($"/api/users/{result.Username}", result)
            : Results.BadRequest("Username ou email já estão em uso");
    }
}