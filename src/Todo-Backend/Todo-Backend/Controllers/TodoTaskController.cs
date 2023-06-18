using Microsoft.AspNetCore.Mvc;
using Todo_Backend.Models;
using Todo_Backend.Services;

namespace Todo_Backend.Controllers;


[ApiController]
[Route("[controller]/[action]")]
public class TodoTaskController : ControllerBase
{
    private readonly ITodoTaskService _taskService;
    
    public TodoTaskController(ITodoTaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet(Name = "GetTasks")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TodoTask>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<TodoTask>>> Get()
    {
        try
        {
            var tasks = await _taskService.GetTasksAsync();
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            // TODO: Log error
            return Problem(detail: ex.StackTrace, title: ex.Message, statusCode: 500);
        }
    }

    [HttpGet(Name = "GetTaskById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoTask))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var tasks = await _taskService.GetTaskById(id);
            return tasks == null ? NotFound() : Ok(tasks);
        }
        catch (Exception ex)
        {
            // TODO: Log error
            return Problem(detail: ex.StackTrace, title: ex.Message, statusCode: 500);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoTask))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddTask([FromBody] string taskDescription)
    {

        try
        {
            if(string.IsNullOrEmpty(taskDescription)){
                return BadRequest();
            }
            var createdTask = await _taskService.AddTask(taskDescription);
            return Ok(createdTask);
        }
        catch (Exception ex)
        {
            // TODO: Log error
            return Problem(detail: ex.StackTrace, title: ex.Message, statusCode: 500);
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoTask))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateTask([FromBody] TodoTask todoTask)
    {

        try
        {
            var updatedTask = await _taskService.UpdateTask(todoTask.Id, todoTask.Description, todoTask.Status);
            return updatedTask == null ? NotFound() :Ok(updatedTask);
        }
        catch (Exception ex)
        {
            // TODO: Log error
            return Problem(detail: ex.StackTrace, title: ex.Message, statusCode: 500);
        }
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoTask))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteTask(int id, bool hardDelete = false)
    {

        try
        {
            var deletedTask = await _taskService.DeleteTask(id, hardDelete);
            return deletedTask == null ? NotFound() : Ok(deletedTask);
        }
        catch (Exception ex)
        {
            // TODO: Log error
            return Problem(detail: ex.StackTrace, title: ex.Message, statusCode: 500);
        }
    }
}