using Colegio.Data;
using Colegio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Colegio.Controllers
{
    public class GeneroController : Controller
    {
        private readonly MyDBContext _myDBContext;

        public GeneroController(MyDBContext myDBContext)
        {
            _myDBContext = myDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _myDBContext.Tab_Genero.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Genero genero)
        {
            try
            {
                if (await _myDBContext.Tab_Genero.AnyAsync(e => e.DescGenero == genero.DescGenero))
                {
                    ModelState.AddModelError("Género", "El género ya existe.");
                }
                if (ModelState.IsValid)
                {
                    _myDBContext.Tab_Genero.Add(genero);
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
                Genero genero = await _myDBContext.Tab_Genero.FindAsync(id);
                if (genero == null)
                {
                    return NotFound();
                }
                return View(genero);
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
                Genero genero = await _myDBContext.Tab_Genero.FindAsync(id);
                if (genero == null)
                {
                    return NotFound();
                }
                return View(genero);
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Editar(Genero genero)
        {
            try
            {
                if (await _myDBContext.Tab_Genero.AnyAsync(e => e.DescGenero == genero.DescGenero))
                {
                    ModelState.AddModelError("Género", "El género ya existe.");
                }
                if (ModelState.IsValid)
                {
                    _myDBContext.Update(genero);
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
            Genero genero = _myDBContext.Tab_Genero.Find(id);
            if (genero == null)
            {
                return NotFound();            
            }
            return View(genero);
        }

        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> Eliminar(Genero genero)
        {
            try { 
            if(_myDBContext != null && _myDBContext.Tab_Genero != null)
                {
                    _myDBContext.Tab_Genero.Remove(genero);
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
