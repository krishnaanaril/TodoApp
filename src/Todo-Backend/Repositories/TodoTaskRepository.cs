using Todo_Backend.Models;

namespace Todo_Backend.Repositories;

public interface ITodoTaskRepository {
    IList<TodoTask> GetTasksAsync();
    TodoTask AddTask(string taskDescription);
    Task SaveChanges();
}

public class TodoTaskRepository : ITodoTaskRepository
{
    private readonly TodoTaskContext _todoTaskContext;

    public TodoTaskRepository(TodoTaskContext todoTaskContext)
    {
        _todoTaskContext = todoTaskContext;
    }
    public IList<TodoTask> GetTasksAsync()
    {
        return _todoTaskContext.TodoTasks.ToList();
    }

    public TodoTask AddTask(string taskDescription){
        var newTask = new TodoTask(taskDescription);
        _todoTaskContext.TodoTasks.Add(newTask);        
        return newTask;
    }

    public Task SaveChanges()
    {
        return _todoTaskContext.SaveChangesAsync();
    }
}