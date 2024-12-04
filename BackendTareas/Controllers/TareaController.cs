using AutoMapper;
using BackendTareas.Models;
using BackendTareas.Models.Dto;
using BackendTareas.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendTareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly ITareaRepositorio _ctRepo;
        private readonly IMapper _mapper;
        public TareaController(ITareaRepositorio ctRepo, IMapper mapper )
        {
            _ctRepo = ctRepo;  
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK )]
        public IActionResult GetTareas()
        {
            var ListaTarea  = _ctRepo.GetTareas();
            var ListaTareasDto = new List<TareaDto>();

            foreach (var lista in ListaTareasDto)
            {
                ListaTareasDto.Add(_mapper.Map<TareaDto>(lista));
            }

            return Ok(ListaTareasDto);
        }

        [HttpGet("{Id:int}", Name = "GetTarea")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetTarea(int Id)
        {
            var itemTarea = _ctRepo.GetTarea(Id);

            if (itemTarea == null) 
            {
                return NotFound();
            }

            var itemTareaDto = _mapper.Map<TareaDto>(itemTarea); 

            return Ok(itemTareaDto);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearTarea([FromBody] CrearTareaDto crearTareaDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(crearTareaDto == null) return BadRequest(ModelState);

            if (_ctRepo.ExisteTarea(crearTareaDto.Title)) {
                ModelState.AddModelError("", "El titulo ya existe");
                return StatusCode(StatusCodes.Status400BadRequest);
            }


            var tarea = _mapper.Map<Tarea>(crearTareaDto);

            if (!_ctRepo.CrearTarea(tarea)) {
                ModelState.AddModelError("", $"no se guardo el registro tarea de titulo: {tarea.Title}");
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return CreatedAtRoute("GetTarea", new { Id = tarea.Id}, tarea);
        }

        [HttpPut("{Id:int}", Name = "ActualizarTarea")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarTarea(int Id, [FromBody] TareaDto tareaDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            if(tareaDto == null || Id != tareaDto.Id) return BadRequest(ModelState);

            var tareaExistente = _ctRepo.GetTarea(Id);

            if (tareaExistente == null) return NotFound($"No se encontro la tarea: {Id}");

            var tarea = _mapper.Map<Tarea>(tareaDto);

            if (!_ctRepo.ActualizarTarea(tarea))
            {
                ModelState.AddModelError("", $"no se actualizo el registro tarea de titulo: {tarea.Title}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{Id:int}", Name = "EliminarTarea")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult EliminarTarea(int Id)
        {
            if (!_ctRepo.ExisteTarea(Id)) return NotFound($"No se encontro la tarea: {Id}");

            var tarea = _ctRepo.GetTarea(Id);

            if (!_ctRepo.BorrarTarea(tarea))
            {
                ModelState.AddModelError("", $"no se encontro tarea que eliminar");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
