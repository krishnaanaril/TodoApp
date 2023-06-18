using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Todo_Backend.Controllers;
using Todo_Backend.Models;
using Todo_Backend.Services;

namespace Todo_Backend.Tests;

[TestFixture]
public class TaskControllerTests
{
    private Mock<ITodoTaskService> taskService;
    private TodoTask mockTask;
    private IList<TodoTask> mockTaskList;
    
    [OneTimeSetUp]
    public void Setup()
    {
        taskService = new Mock<ITodoTaskService>();
        mockTaskList = new List<TodoTask>() {
            new TodoTask("Wake Up at 6 AM") 
            { 
                Id= 1,                 
                CreatedTime= new DateTime(2023, 06, 16, 16, 0, 0), 
                UpdatedTime =  new DateTime(2023, 06, 16, 16, 0, 0), 
                Status = TodoTaskStatus.InProgress
            },
            new TodoTask("Take dotnet session") 
            { 
                Id= 1,                 
                CreatedTime= new DateTime(2023, 06, 17, 12, 0, 0), 
                UpdatedTime =  new DateTime(2023, 06, 17, 12, 0, 0), 
                Status = TodoTaskStatus.New
            }
        };
        mockTask = mockTaskList[0];        
    }

    [Test]
    public async Task Get_AllTask_ReturnsListOfTasks()
    {
        // Arrange
        taskService.Setup(service => service.GetTasksAsync()).Returns(Task.FromResult(mockTaskList));
        TodoTaskController taskController = new(taskService.Object);

        // Act
        var actionResult = await taskController.Get() ;
        var result = actionResult.Result as OkObjectResult;
        var resultValue = result!.Value as IList<TodoTask>;

        // Assert
        Assert.Multiple(()=>
        {
            Assert.That(result.Value, Is.Not.Null);            
            Assert.That(resultValue, Has.Count.EqualTo(2));
            Assert.That(result!.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        });
    }

    [Test]
    public async Task Get_AllTask_ReturnsException()
    {
        taskService.Setup(service => service.GetTasksAsync()).ThrowsAsync(new Exception("Some message here"));
        TodoTaskController taskController = new(taskService.Object);
        
        // Act
        var actionResult = await taskController.Get() ;        
        var result = actionResult.Result as ObjectResult;

        // Assert
        Assert.That(result!.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
    }
}