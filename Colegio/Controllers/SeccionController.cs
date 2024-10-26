using Colegio.Data;
using Colegio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Colegio.Controllers
{
    public class SeccionController : Controller
    {
        private readonly MyDBContext _myDBContext;

        public SeccionController(MyDBContext myDBContext)
        {
            _myDBContext = myDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _myDBContext.Tab_Seccion.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Seccion seccion)
        {
            try
            {
                if (await _myDBContext.Tab_Seccion.AnyAsync(e => e.DescSeccion == seccion.DescSeccion))
                {
                    ModelState.AddModelError("Seccion", "La sección ya existe.");
                }
                if (ModelState.IsValid)
                {
                    _myDBContext.Tab_Seccion.Add(seccion);
                    await _myDBContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
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
                Seccion seccion = await _myDBContext.Tab_Seccion.FindAsync(id);
                if (seccion == null)
                {
                    return NotFound();
                }
                return View(seccion);
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
                Seccion seccion = await _myDBContext.Tab_Seccion.FindAsync(id);
                if (seccion == null)
                {
                    return NotFound();
                }
                return View(seccion);
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Editar(Seccion seccion)
        {
            try
            {
                if (await _myDBContext.Tab_Seccion.AnyAsync(e => e.DescSeccion == seccion.DescSeccion))
                {
                    ModelState.AddModelError("Seccion", "La sección ya existe.");
                }
                if (ModelState.IsValid)
                {
                    _myDBContext.Update(seccion);
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

        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Seccion seccion = _myDBContext.Tab_Seccion.Find(id);
            if (seccion == null)
            {
                return NotFound();            
            }
            return View(seccion);
        }

        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> Eliminar(Seccion seccion)
        {
            try { 
            if(_myDBContext != null && _myDBContext.Tab_Seccion != null)
                {
                    _myDBContext.Tab_Seccion.Remove(seccion);
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
