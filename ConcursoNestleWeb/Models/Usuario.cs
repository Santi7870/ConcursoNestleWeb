using System.ComponentModel.DataAnnotations;

namespace ConcursoNestleWeb.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Clave { get; set; }
    }
}
