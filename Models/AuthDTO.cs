namespace TaskManager.Api.Models;

public record LoginRequest(string Username, string Password);

public record RegisterRequest(string Username, string Email, string Password);

public record AuthResponse(string Token, string Username, string Email);

public class AuthDTO
{
    
}