using BackendTareas.Models;

namespace BackendTareas.Repositorio.IRepositorio
{
    public interface ITareaRepositorio
    {

        ICollection<Tarea> GetTareas();

        Tarea GetTarea(int id);

        bool ExisteTarea(int id);

        bool ExisteTarea(string title);

        bool CrearTarea(Tarea tarea);

        bool ActualizarTarea(Tarea tarea);

        bool BorrarTarea(Tarea tarea);

        bool Guardar();

    }
}
