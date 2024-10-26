using System.ComponentModel.DataAnnotations;

namespace Colegio.Models
{
    public class Seccion
    {
        public int Id { get; set; }

        [Display(Name ="Sección")]
        [Required(ErrorMessage ="La sección es obligatoria")]
        [StringLength(50)]
        public string DescSeccion { get; set; } = string.Empty;
    }
}
