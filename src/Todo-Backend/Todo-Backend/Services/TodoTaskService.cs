using Todo_Backend.Models;
using Todo_Backend.Repositories;

namespace Todo_Backend.Services;

public interface ITodoTaskService
{
    Task<IList<TodoTask>> GetTasksAsync();
    Task<TodoTask?> GetTaskById(int id);
    Task<TodoTask?> AddTask(string taskDescription);
    Task<TodoTask?> UpdateTask(int id, string taskDescription, TodoTaskStatus taskStatus);
    Task<TodoTask?> DeleteTask(int id, bool hardDelete = false);
}

public class TodoTaskService : ITodoTaskService
{
    private readonly ITodoTaskRepository _todoTaskRepository;
    public TodoTaskService(ITodoTaskRepository todoTaskRepository )
    {
        _todoTaskRepository = todoTaskRepository;
    }
    public Task<IList<TodoTask>> GetTasksAsync()
    {
        var tasks = _todoTaskRepository.GetTasksAsync();
        return Task.FromResult( tasks );
    }

    public Task<TodoTask?> GetTaskById(int id){
        var selectedTask = _todoTaskRepository.GetTaskById(id);
        return Task.FromResult(selectedTask);
    }

    public async Task<TodoTask?> AddTask(string taskDescription){
        var createdTask = _todoTaskRepository.AddTask(taskDescription);
        if(createdTask != null)
        {
            await _todoTaskRepository.SaveChanges();
        } 
        return createdTask;
    }

    public async Task<TodoTask?> UpdateTask(int id, string taskDescription, TodoTaskStatus taskStatus)
    {
        var updatedTask = _todoTaskRepository.UpdateTask(id, taskDescription, taskStatus);
        if(updatedTask != null)
        {
            await _todoTaskRepository.SaveChanges();
        }   
        return updatedTask;
    }

    public async Task<TodoTask?> DeleteTask(int id, bool hardDelete = false)
    {
        TodoTask? deletedTask;
        if(hardDelete)
        {
            deletedTask = _todoTaskRepository.DeleteTask(id);
        }
        else 
        {
            deletedTask = _todoTaskRepository.MarkAsDeleted(id);
        }

        if(deletedTask != null)
        {
            await _todoTaskRepository.SaveChanges();
        }   
        return deletedTask;
    }
}