using BackendTareas.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendTareas.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
                
        }

        public DbSet<Tarea> Tareas { get; set; }
        public object Tarea { get; internal set; }
    }
}
