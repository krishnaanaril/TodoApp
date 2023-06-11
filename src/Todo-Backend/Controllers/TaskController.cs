using Microsoft.AspNetCore.Mvc;
using Todo_Backend.Models;
using Todo_Backend.Services;

namespace Todo_Backend.Controllers;


[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITodoTaskService _taskService;
    public TaskController(ITodoTaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet(Name = "GetTasks")]
    public async Task<IEnumerable<TodoTask>> Get()
    {
        var tasks = await _taskService.GetTasksAsync();
        return tasks;
    }

    [HttpPost]
    public async Task<TodoTask> AddTask([FromBody]string taskDescription)
    {
        var createdTask = await _taskService.AddTask(taskDescription);
        return createdTask;
    }
}