namespace API_con_.NET.Models;

public class Task
{
    public Guid TaskId {get; set;}
    public Guid CategoryId {get; set;}
    public string Title {get; set;}    
    public string Description {get; set;}
    public Priority TaskPriority {get; set;}    
    public DateTime CreationDate {get; set;}
    public virtual Category Category {get; set;}        
    public string Summary {get; set;} //resumen        
}

public enum Priority
{
    low,
    medium,
    high,
}