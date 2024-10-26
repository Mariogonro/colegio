using Colegio.Data;
using Colegio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Colegio.Controllers
{
    public class AlumnoController : Controller
    {
        private readonly MyDBContext _myDBContext;

        public AlumnoController(MyDBContext myDBContext)
        {
            _myDBContext = myDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _myDBContext.Tab_Alumno.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            ViewBag.Genero = new SelectList(await _myDBContext.Tab_Genero.ToListAsync(), "Id", "DescGenero");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Alumno alumno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _myDBContext.Tab_Alumno.Add(alumno);
                    await _myDBContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Genero = new SelectList(await _myDBContext.Tab_Genero.ToListAsync(), "Id", "DescGenero");
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
                var alumno = await _myDBContext.Tab_Alumno
                    .Include(u => u.Genero)
                    .FirstOrDefaultAsync(u => u.Id == id);
                if (alumno == null)
                {
                    return NotFound();
                }
                return View(alumno);
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
                Alumno alumno = await _myDBContext.Tab_Alumno.FindAsync(id);
                if (alumno == null)
                {
                    return NotFound();
                }
                ViewBag.Genero = new SelectList(await _myDBContext.Tab_Genero.ToListAsync(), "Id", "DescGenero", alumno.IdGenero);
                return View(alumno);
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Editar(Alumno alumno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _myDBContext.Update(alumno);
                    await _myDBContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Genero = new SelectList(await _myDBContext.Tab_Genero.ToListAsync(), "Id", "DescGenero", alumno.IdGenero);
            }
            catch (Exception e)
            {
                return NotFound();
            }
            return View();
        }

        [HttpGet]
        public async Task <IActionResult> Eliminar(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var alumno = await _myDBContext.Tab_Alumno
                    .Include(u => u.Genero)
                    .FirstOrDefaultAsync(u => u.Id == id);

            if (alumno == null)
            {
                return NotFound();            
            }
            return View(alumno);
        }

        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> Eliminar(Alumno alumno)
        {
            try { 
            if(_myDBContext != null && _myDBContext.Tab_Alumno != null)
                {
                    _myDBContext.Tab_Alumno.Remove(alumno);
                    await _myDBContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(Exception e)
            {
                return NotFound();
            }
            return View();
        }
        [ResponseCache (Duration = 0,Location = ResponseCacheLocation.None,NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
