using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo_Backend.Models;

public class TodoTask 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get;set;}
    public string Description {get;set;}
    public TodoTaskStatus Status {get;set;}
    public DateTime CreatedTime {get;set;}

    public TodoTask(string description)
    {
        Description = description;
        Status = TodoTaskStatus.New;
        CreatedTime= DateTime.Now;
    }
}