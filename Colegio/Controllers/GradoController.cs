using Colegio.Data;
using Colegio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Colegio.Controllers
{
    public class GradoController : Controller
    {
        private readonly MyDBContext _myDBContext;

        public GradoController(MyDBContext myDBContext)
        {
            _myDBContext = myDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {


            var grados = await _myDBContext.Tab_Grado.ToListAsync();

            return View(grados);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var profesores = await _myDBContext.Tab_Profesor
                .Select(p => new
                {
                    p.Id,
                    NombreCompleto = p.Nombre + " " + p.Apellido
                })
                .ToListAsync();

            ViewBag.Profesor = new SelectList(profesores, "Id", "NombreCompleto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Grado grado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _myDBContext.Tab_Grado.Add(grado);
                    await _myDBContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                var profesores = await _myDBContext.Tab_Profesor
                .Select(p => new
                {
                    p.Id,
                    NombreCompleto = p.Nombre + " " + p.Apellido
                })
                .ToListAsync();

                ViewBag.Profesor = new SelectList(profesores, "Id", "NombreCompleto");
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
                var grado = await _myDBContext.Tab_Grado
                    .Include(u => u.Profesor)
                    .FirstOrDefaultAsync(u => u.Id == id);
                if (grado == null)
                {
                    return NotFound();
                }
                return View(grado);
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
                var profesores = await _myDBContext.Tab_Profesor
               .Select(p => new
               {
                   p.Id,
                   NombreCompleto = p.Nombre + " " + p.Apellido
               })
               .ToListAsync();


                Grado grado = await _myDBContext.Tab_Grado.FindAsync(id);
                if (grado == null)
                {
                    return NotFound();
                }

                ViewBag.Profesor = new SelectList(profesores, "Id", "NombreCompleto", grado.IdProfesor);
                return View(grado);
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Editar(Grado grado)
        {
            try
            {
                var profesores = await _myDBContext.Tab_Profesor
            .Select(p => new
            {
                p.Id,
                NombreCompleto = p.Nombre + " " + p.Apellido
            })
            .ToListAsync();


                if (ModelState.IsValid)
                {
                    _myDBContext.Update(grado);
                    await _myDBContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Profesor = new SelectList(profesores, "Id", "NombreCompleto", grado.IdProfesor);
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
            var grado = await _myDBContext.Tab_Grado
                    .Include(u => u.Profesor)
                    .FirstOrDefaultAsync(u => u.Id == id);

            if (grado == null)
            {
                return NotFound();
            }
            return View(grado);
        }

        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> Eliminar(Grado grado)
        {
            try
            {
                if (_myDBContext != null && _myDBContext.Tab_Grado != null)
                {
                    _myDBContext.Tab_Grado.Remove(grado);
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
