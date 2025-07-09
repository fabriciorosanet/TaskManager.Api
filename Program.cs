using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Config Entity Framework Core with SQLite
builder.Services.AddDbContext<TaskContext>(options =>
    options.UseSqlite("Data Source=tasks.db"));

// Config Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Config pipeline 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Criate the database if it doesn't exist
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TaskContext>();
    dbContext.Database.EnsureCreated(); 
}

app.MapGet("/", () => "Task Manager API is running!");

app.Run();
