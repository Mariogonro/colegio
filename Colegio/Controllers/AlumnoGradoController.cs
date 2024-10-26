using Colegio.Data;
using Colegio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Colegio.Controllers
{
    public class AlumnoGradoController : Controller
    {
        private readonly MyDBContext _myDBContext;

        public AlumnoGradoController(MyDBContext myDBContext)
        {
            _myDBContext = myDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var alumnoGrados = await _myDBContext.Tab_AlumnoGrado
                .Include(ag => ag.Alumno)
                .Include(ag => ag.Grado)
                .Include(ag => ag.Seccion)
                .ToListAsync();
            return View(alumnoGrados);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var alumnos = await _myDBContext.Tab_Alumno
                .Select(a => new
                {
                    a.Id,
                    NombreCompleto = a.Nombre + " " + a.Apellido
                })
                .ToListAsync();

            ViewBag.Alumno = new SelectList(alumnos, "Id", "NombreCompleto");
            ViewBag.Grado = new SelectList(await _myDBContext.Tab_Grado.ToListAsync(), "Id", "Nombre");
            ViewBag.Seccion = new SelectList(await _myDBContext.Tab_Seccion.ToListAsync(), "Id", "DescSeccion");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(AlumnoGrado alumnoGrado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _myDBContext.Tab_AlumnoGrado.Add(alumnoGrado);
                    await _myDBContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                var alumno = await _myDBContext.Tab_Alumno
                 .Select(a => new
                 {
                     a.Id,
                     NombreCompleto = a.Nombre + " " + a.Apellido
                 })
                 .ToListAsync();

                ViewBag.Alumno = new SelectList(alumno, "Id", "NombreCompleto");
                ViewBag.Grado = new SelectList(await _myDBContext.Tab_Profesor.ToListAsync(), "Id", "Nombre");
                ViewBag.Seccion = new SelectList(await _myDBContext.Tab_Profesor.ToListAsync(), "Id", "DescSeccion");
                return View();
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                AlumnoGrado alumnoGrado = await _myDBContext.Tab_AlumnoGrado.FindAsync(id);
                Alumno alumno = await _myDBContext.Tab_Alumno.FindAsync(alumnoGrado.IdAlumno);
                alumnoGrado.Alumno.Nombre = alumno.Nombre;
                alumnoGrado.Alumno.Apellido = alumno.Apellido;

                Grado grado = await _myDBContext.Tab_Grado.FindAsync(alumnoGrado.IdGrado);
                alumnoGrado.Grado.Nombre = grado.Nombre;

                Seccion seccion = await _myDBContext.Tab_Seccion.FindAsync(alumnoGrado.IdSeccion);
                alumnoGrado.Seccion.DescSeccion = seccion.DescSeccion;

                if (alumnoGrado == null)
                {
                    return NotFound();
                }

                return View(alumnoGrado);
            }

            catch (Exception e)
            {
                return NotFound();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                AlumnoGrado alumnoGrado = await _myDBContext.Tab_AlumnoGrado.FindAsync(id);
                if (alumnoGrado == null)
                {
                    return NotFound();
                }
                var alumnos = await _myDBContext.Tab_Alumno
                .Select(a => new
                {
                    a.Id,
                    NombreCompleto = a.Nombre + " " + a.Apellido
                })
                .ToListAsync();

                ViewBag.Alumno = new SelectList(alumnos, "Id", "NombreCompleto", alumnoGrado.IdAlumno);
                ViewBag.Grado = new SelectList(await _myDBContext.Tab_Grado.ToListAsync(), "Id", "Nombre", alumnoGrado.IdGrado);
                ViewBag.Seccion = new SelectList(await _myDBContext.Tab_Seccion.ToListAsync(), "Id", "DescSeccion", alumnoGrado.IdSeccion);
                return View(alumnoGrado);
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Editar(AlumnoGrado alumnoGrado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _myDBContext.Update(alumnoGrado);
                    await _myDBContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                var alumnos = await _myDBContext.Tab_Alumno
                .Select(a => new
                {
                    a.Id,
                    NombreCompleto = a.Nombre + " " + a.Apellido
                })
                .ToListAsync();

                ViewBag.Alumno = new SelectList(alumnos, "Id", "NombreCompleto", alumnoGrado.IdAlumno);
                ViewBag.Grado = new SelectList(await _myDBContext.Tab_Grado.ToListAsync(), "Id", "Nombre", alumnoGrado.IdGrado);
                ViewBag.Seccion = new SelectList(await _myDBContext.Tab_Seccion.ToListAsync(), "Id", "DescSeccion", alumnoGrado.IdSeccion);
            }
            catch (Exception e)
            {
                return NotFound();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            if (id == null)
            {
                return NotFound();
            }


            AlumnoGrado alumnoGrado = await _myDBContext.Tab_AlumnoGrado.FindAsync(id);
            Alumno alumno = await _myDBContext.Tab_Alumno.FindAsync(alumnoGrado.IdAlumno);
            alumnoGrado.Alumno.Nombre = alumno.Nombre;
            alumnoGrado.Alumno.Apellido = alumno.Apellido;

            Grado grado = await _myDBContext.Tab_Grado.FindAsync(alumnoGrado.IdGrado);
            alumnoGrado.Grado.Nombre = grado.Nombre;

            Seccion seccion = await _myDBContext.Tab_Seccion.FindAsync(alumnoGrado.IdSeccion);
            alumnoGrado.Seccion.DescSeccion = seccion.DescSeccion;
            if (alumnoGrado == null)
            {
                return NotFound();
            }
            return View(alumnoGrado);
        }

        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> Eliminar(AlumnoGrado alumnoGrado)
        {
            try
            {
                if (_myDBContext != null && _myDBContext.Tab_AlumnoGrado != null)
                {
                    _myDBContext.Tab_AlumnoGrado.Remove(alumnoGrado);
                    await _myDBContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                return NotFound();
            }
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
