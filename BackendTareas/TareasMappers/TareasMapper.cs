using AutoMapper;
using BackendTareas.Models;
using BackendTareas.Models.Dto;

namespace BackendTareas.TareasMapper
{
    public class TareasMapper: Profile
    {

        public TareasMapper()
        {
            CreateMap<Tarea, TareaDto>().ReverseMap();
            CreateMap<Tarea, CrearTareaDto>().ReverseMap();
        }

    }
}
