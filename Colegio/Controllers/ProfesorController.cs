using Colegio.Data;
using Colegio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Colegio.Controllers
{
    public class ProfesorController : Controller
    {
        private readonly MyDBContext _myDBContext;

        public ProfesorController(MyDBContext myDBContext)
        {
            _myDBContext = myDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _myDBContext.Tab_Profesor.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            ViewBag.Genero = new SelectList(await _myDBContext.Tab_Genero.ToListAsync(), "Id", "DescGenero");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Profesor profesor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _myDBContext.Tab_Profesor.Add(profesor);
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
                var profesor = await _myDBContext.Tab_Profesor
                    .Include(u => u.Genero)
                    .FirstOrDefaultAsync(u => u.Id == id);
                if (profesor == null)
                {
                    return NotFound();
                }
                return View(profesor);
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
                Profesor profesor = await _myDBContext.Tab_Profesor.FindAsync(id);
                if (profesor == null)
                {
                    return NotFound();
                }
                ViewBag.Genero = new SelectList(await _myDBContext.Tab_Genero.ToListAsync(), "Id", "DescGenero", profesor.IdGenero);
                return View(profesor);
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Editar(Profesor profesor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _myDBContext.Update(profesor);
                    await _myDBContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Genero = new SelectList(await _myDBContext.Tab_Genero.ToListAsync(), "Id", "DescGenero", profesor.IdGenero);
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
            var profesor = await _myDBContext.Tab_Profesor
                    .Include(u => u.Genero)
                    .FirstOrDefaultAsync(u => u.Id == id);

            if (profesor == null)
            {
                return NotFound();            
            }
            return View(profesor);
        }

        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> Eliminar(Profesor profesor)
        {
            try { 
            if(_myDBContext != null && _myDBContext.Tab_Profesor != null)
                {
                    _myDBContext.Tab_Profesor.Remove(profesor);
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
