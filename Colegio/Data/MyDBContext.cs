using Colegio.Models;
using Microsoft.EntityFrameworkCore;

namespace Colegio.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Alumno> Tab_Alumno { get; set; }
        public DbSet<AlumnoGrado> Tab_AlumnoGrado { get; set; }
        public DbSet<Genero> Tab_Genero { get; set; }
        public DbSet<Grado> Tab_Grado { get; set; }
        public DbSet<Profesor> Tab_Profesor { get; set; }
        public DbSet<Seccion> Tab_Seccion { get; set; }
    }
}
