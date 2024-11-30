using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BackendTareas.Models
{
    public class Tarea
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }

        public string Description { get; set; }

        [DefaultValue(false)]
        public bool IsCompleted { get; set; }

        public DateTime CreateAt { get; set; }

        public Tarea() 
        {
            CreateAt = DateTime.Now;
        }

    }
}
