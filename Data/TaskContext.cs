using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Models;
namespace TaskManager.Api.Data;

public class TaskContext : DbContext
{
    public TaskContext(DbContextOptions<TaskContext> options) : base(options){}
    public DbSet<TaskItem> Tasks { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>().ToTable("Tasks");
        modelBuilder.Entity<TaskItem>().HasKey(t => t.Id);
        modelBuilder.Entity<TaskItem>().Property(t => t.Title).IsRequired().HasMaxLength(200);
        modelBuilder.Entity<TaskItem>().Property(t => t.Description).HasMaxLength(1000);
        modelBuilder.Entity<TaskItem>().Property(t => t.IsCompleted).IsRequired();
        modelBuilder.Entity<TaskItem>().Property(t => t.CreatedAt).IsRequired().HasDefaultValueSql("datetime('now')");
    }
    
}