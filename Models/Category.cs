using System.Text.Json.Serialization;

namespace API_con_.NET.Models;

public class Category
{
    public Guid CategoryId {get; set;}
    public string Name {get; set;}
    public string Description {get; set;}

    public int ImportanceLevel {get ; set;}

    [JsonIgnore] //al momento de retornar los datos,no traiga la coleccion de tarea..
    public virtual ICollection<Task> Tasks {get; set;} 
}