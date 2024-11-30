using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BackendTareas.Models.Dto
{
    public class TareaDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El titulo es obligatorio")]
        public required string Title { get; set; }

        [MaxLength(256, ErrorMessage = "Numero maximo de caracteres es de 256 !")]
        public string Description { get; set; }

        [DefaultValue(false)]
        public bool IsCompleted { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
