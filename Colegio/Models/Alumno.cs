using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Models
{
    public class Alumno
    {
        public int Id { get; set; }

        [Display(Name ="Nombres")]
        [Required(ErrorMessage ="Debe ingresar los nombres")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "Debe ingresar los apellidos")]
        public string Apellido { get; set; } = string.Empty;

        [Display(Name = "Género")]
        [Required(ErrorMessage = "Debe seleccionar un género")]
        public int IdGenero { get; set; }

        [ForeignKey ("IdGenero")]
        public Genero? Genero { get; set; }

        [Display(Name = "Fecha Nacimiento")]
        [Required(ErrorMessage = "Debe ingresar la fecha de nacimiento")]
        [DataType(DataType.Date)]
        public DateTime FechaNac { get; set; }

    }
}
