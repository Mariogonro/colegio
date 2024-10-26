using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Models
{
    public class AlumnoGrado
    {
        public int Id { get; set; }

        [Display(Name = "Alumno")]
        [Required(ErrorMessage = "Debe seleccionar un Alumno")]
        public int IdAlumno { get; set; }

        [ForeignKey("IdAlumno")]
        public Alumno? Alumno { get; set; }


        [Display(Name = "Grado")]
        [Required(ErrorMessage = "Debe seleccionar un Grado")]
        public int IdGrado { get; set; }

        [ForeignKey ("IdGrado")]
        public Grado? Grado { get; set; }
        
        [Display(Name = "Sección")]
        [Required(ErrorMessage = "Debe seleccionar una sección")]
        public int IdSeccion { get; set; }

        [ForeignKey ("IdSeccion")]
        public Seccion? Seccion { get; set; } 
        
                
    }
}
