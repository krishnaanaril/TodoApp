using Todo_Backend.Models;

namespace Todo_Backend.Repositories;

public interface ITodoTaskRepository {
    IList<TodoTask> GetTasksAsync();
    TodoTask? GetTaskById(int id);
    TodoTask? AddTask(string taskDescription);
    TodoTask? UpdateTask(int id, string taskDescription, TodoTaskStatus taskStatus);
    TodoTask? MarkAsDeleted(int id);
    TodoTask? DeleteTask(int id);
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

    public TodoTask? GetTaskById(int id)
    {
        var selectedTask = _todoTaskContext.TodoTasks.FirstOrDefault(task => task.Id == id);
        return selectedTask;
    }

    public TodoTask? AddTask(string taskDescription){
        TodoTask? newTask = new TodoTask(taskDescription);
        try
        {
            _todoTaskContext.TodoTasks.Add(newTask);  
        }
        catch 
        {           
            newTask = null;
        }      
        return newTask;
    }

    public TodoTask? UpdateTask(int id, string taskDescription, TodoTaskStatus taskStatus){
        var taskToBeUpdated = _todoTaskContext.TodoTasks.FirstOrDefault(task => task.Id == id);
        if(taskToBeUpdated != null){
            taskToBeUpdated.Description = taskDescription;
            taskToBeUpdated.Status = taskStatus;
            taskToBeUpdated.UpdatedTime = DateTime.Now;
        }
        return taskToBeUpdated;
    }

    public TodoTask? MarkAsDeleted(int id){
        var taskToBeMarked = _todoTaskContext.TodoTasks.FirstOrDefault(task => task.Id == id);
        if(taskToBeMarked != null){
            taskToBeMarked.Status = TodoTaskStatus.Deleted;
            taskToBeMarked.UpdatedTime = DateTime.Now;
        }
        return taskToBeMarked;
    }

    public TodoTask? DeleteTask(int id){
        var taskToBeDeleted = _todoTaskContext.TodoTasks.FirstOrDefault(task => task.Id == id);        
        if(taskToBeDeleted != null){
            _todoTaskContext.TodoTasks.Remove(taskToBeDeleted);
        }
        return taskToBeDeleted;
    }

    public Task SaveChanges()
    {
        return _todoTaskContext.SaveChangesAsync();
    }
}