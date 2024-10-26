using System.ComponentModel.DataAnnotations;

namespace Colegio.Models
{
    public class Genero
    {
        public int Id { get; set; }

        [Display(Name ="Género")]
        [Required(ErrorMessage ="El género es obligatorio")]
        [StringLength(50)]
        public string DescGenero { get; set; } = string.Empty;
    }
}
