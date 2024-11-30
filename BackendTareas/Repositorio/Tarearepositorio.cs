using BackendTareas.Context;
using BackendTareas.Models;
using BackendTareas.Repositorio.IRepositorio;

namespace BackendTareas.Repositorio
{
    public class Tarearepositorio: ITareaRepositorio
    {
        private readonly AppDbContext _db;

        public Tarearepositorio(AppDbContext db)
        {
            _db = db;
        }

        public bool ActualizarTarea(Tarea tarea)
        {
            tarea.CreateAt = DateTime.Now;
            _db.Tareas.Update(tarea);
            return Guardar();
        }

        public bool BorrarTarea(Tarea tarea)
        {
            _db.Tareas.Remove(tarea);
            return Guardar();
        }

        public bool CrearTarea(Tarea tarea)
        {
            tarea.CreateAt = DateTime.Now;
            _db.Tareas.Add(tarea);
            return Guardar();
        }

        public bool ExisteTarea(int id)
        {
            return _db.Tareas.Any(t => t.Id == id);
        }

        public bool ExisteTarea(string title)
        {
            bool valor = _db.Tareas.Any(t =>t.Title.ToLower().Trim() == title.ToLower().Trim());
            return valor;
        }

        public Tarea GetTarea(int id)
        {
            return _db.Tareas.FirstOrDefault(t => t.Id == id);
        }

        public ICollection<Tarea> GetTareas()
        {
            return _db.Tareas.OrderBy(t => t.Id).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
