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
        TaskController taskController = new TaskController(taskService.Object);

        // Act
        var result = await taskController.Get() ;

        // Assert
        Assert.That((result as OkObjectResult).StatusCode, Is.EqualTo(StatusCodes.Status200OK));
    }

    [Test]
    public async Task Get_AllTask_ReturnsException()
    {
        taskService.Setup(service => service.GetTasksAsync()).ThrowsAsync(new Exception("Some message here"));
        TaskController taskController = new TaskController(taskService.Object);
        // var ex = Assert.Throws<Exception>(async () => await taskController.Get() );
        // Assert.That(ex.Message, Is.EqualTo("Some message here"));
        Assert.ThrowsAsync<Exception>(async () => await taskController.Get());
    }
}