using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Models
{
    public class Grado
    {
        public int Id { get; set; }

        [Display(Name ="Nombre")]
        [Required(ErrorMessage ="Debe ingresar el nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Profesor")]
        [Required(ErrorMessage = "Debe seleccionar un Profesor")]
        public int IdProfesor { get; set; }

        [ForeignKey ("IdProfesor")]
        public Profesor? Profesor { get; set; }

        
    }
}
