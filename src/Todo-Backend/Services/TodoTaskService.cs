using Todo_Backend.Models;
using Todo_Backend.Repositories;

namespace Todo_Backend.Services;

public interface ITodoTaskService
{
    Task<IList<TodoTask>> GetTasksAsync();
    Task<TodoTask> AddTask(string taskDescription);
}

public class TodoTaskService : ITodoTaskService
{
    private ITodoTaskRepository _todoTaskRepository;
    public TodoTaskService(ITodoTaskRepository todoTaskRepository )
    {
        _todoTaskRepository = todoTaskRepository;
    }
    public Task<IList<TodoTask>> GetTasksAsync()
    {
        var tasks = _todoTaskRepository.GetTasksAsync();
        return Task.FromResult( tasks );
    }

    public async Task<TodoTask> AddTask(string taskDescription){
        var createdTask = _todoTaskRepository.AddTask(taskDescription);
        await _todoTaskRepository.SaveChanges();
        return createdTask;
    }
}