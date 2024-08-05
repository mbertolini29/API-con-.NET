using Microsoft.EntityFrameworkCore;

namespace API_con_.NET.Models;

public class TaskContext : DbContext
{
    //colecciones dentro del Dbc
    //representan toda la coleccion de todos los datos 
    //que se encuentran dentro de ese modelo.
    //que seria la tabla de la base de datos. 
    public DbSet<Category> Categories { get; set; }            
    public DbSet<Task> Tasks { get; set; }   

    public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

    //esto reemplaza a los atributos..
    //utilizando fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Category> categoriesInit = new List<Category>();

        categoriesInit.Add(new Category {
                                            CategoryId = Guid.Parse("ea868af2-32d8-4e70-96b8-abbcdb79bb3b"),
                                            Name = "Actividades pendientes", 
                                            ImportanceLevel = 20 //peso de la categoria..
                                        });

        categoriesInit.Add(new Category {
                                            CategoryId = Guid.Parse("ea868af2-32d8-4e70-96b8-abbcdb79bb02"),
                                            Name = "Actividades personales", 
                                            ImportanceLevel = 50 
                                        });

        //base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Category>(category=>
        {
            category.ToTable("Category");
            category.HasKey(p => p.CategoryId);                
            category.Property(p => p.Name).IsRequired().HasMaxLength(150);
            category.Property(p => p.Description).IsRequired(false);
            category.Property(p => p.ImportanceLevel);
            category.HasData(categoriesInit); //lista de datos..                

        });

        List<Task> TaskInit = new List<Task>();

        TaskInit.Add(new Task  {   
                                    TaskId = Guid.Parse("ea868af2-32d8-4e70-96b8-abbcdb79bb10"),
                                    CategoryId = Guid.Parse("ea868af2-32d8-4e70-96b8-abbcdb79bb3b"),
                                    TaskPriority = Priority.medium, 
                                    Title = "Pago de servicios publicos",
                                    CreationDate = DateTime.Now
                                });
        TaskInit.Add(new Task  {   
                                    TaskId = Guid.Parse("ea868af2-32d8-4e70-96b8-abbcdb79bb11"),
                                    CategoryId = Guid.Parse("ea868af2-32d8-4e70-96b8-abbcdb79bb02"),
                                    TaskPriority = Priority.low, 
                                    Title = "Terminar de ver pelicula en neflix",
                                    CreationDate = DateTime.Now
                                });
        
        modelBuilder.Entity<Task>(Task=>
        {
            Task.ToTable("Task");
            Task.HasKey(p => p.TaskId);
            Task.HasOne(p => p.Category).WithMany(p => p.Tasks).HasForeignKey(p=>p.CategoryId);
            Task.Property(p => p.Title).IsRequired().HasMaxLength(200);
            Task.Property(p => p.Description).IsRequired(false);
            Task.Property(p => p.TaskPriority);
            Task.Property(p => p.CreationDate);
            Task.Ignore(p => p.Summary); //para que no se cree en la base de datos.
            Task.HasData(TaskInit);
        });
    }
}