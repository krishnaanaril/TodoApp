using Microsoft.EntityFrameworkCore;

namespace Todo_Backend.Models;

public class TodoTaskContext : DbContext
{
    public DbSet<TodoTask> TodoTasks { get; set; }    

    public string DbPath { get; }

    public TodoTaskContext()
    {        
        var path = @"E:\KrishnaMohan\Workspace\TodoApp\src\Todo-Backend\Database";
        DbPath = System.IO.Path.Join(path, "tasks.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}