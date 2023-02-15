using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TuLote.Aplicacion;
using TuLote.Entidades;
using TuLote.Entidades.DTOs;
using TuLote.Servicios;

namespace TuLote.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class BarriosController : Controller
    {

        private readonly IServicio_API_Localidad _API_Localidad;
        private readonly IServicio_API_Municipio _API_Municipio;
        private readonly IBarrioRepositorio<Barrio> _barrioRepositorio;
        private readonly IMapper _mapper;
        private readonly IAplicacion<Barrio> _aplicacion;

        public BarriosController(IAplicacion<Barrio> aplicacion, IServicio_API_Localidad API_localidad,
            IServicio_API_Municipio API_Municipio, IBarrioRepositorio<Barrio> barrioRepositorio, IMapper mapper)
        {
            _aplicacion = aplicacion;
            _API_Localidad = API_localidad;
            _API_Municipio = API_Municipio;
            _barrioRepositorio = barrioRepositorio;
            _mapper = mapper;
        }

        // GET: Barrios
        public async Task<IActionResult> Index()
        {
            //var listaBarrios = await _context.Barrios.Include(b => b.Localidad).Include(m => m.Localidad.Municipio).ToListAsync();
            var lista = await _aplicacion.GetAll(b => b.Localidad, b => b.Localidad.Municipio).ToListAsync();
            var listaBarrios = _mapper.Map<List<BarrioDTO>>(lista);//Aquí se hace el mapeo
            return View(listaBarrios);
        }

        // GET: Barrios/Create
        public IActionResult Create()
        {

            ViewBag.listaMunicipios = _API_Municipio.Lista().Result.OrderBy(m => m.Nombre).ToList();
            return View();
        }

        // POST: Barrios/Create

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(BarrioCreacionDTO barrioCreacion)
        {
            var barriosActuales = _aplicacion.GetAll();
            ViewBag.barrio = "Registro exitente";
            Barrio barrio = _mapper.Map<Barrio>(barrioCreacion);
            if (ModelState.IsValid)
            {
                if (!barriosActuales.Result.Any(o => o.Nombre == barrioCreacion.Nombre))
                {
                    await _aplicacion.Add(barrio);
                    TempData["success"] = "Registro creado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.listaMunicipios = _API_Municipio.Lista().Result.OrderBy(m => m.Nombre).ToList();
            return View(barrio);
        }

        public JsonResult GetLocalidadesById(int idMunicipio)
        {
            List<Localidad> localidades = new List<Localidad>();

            localidades = _API_Localidad.Lista().Result.Where(x => x.Municipio.Id == idMunicipio).OrderBy(l => l.Nombre).ToList();
            localidades.Insert(0, new Localidad
            {
                Id = 0,
                Nombre = "Por favor seleccione un municipio"
            });

            return Json(localidades);
        }

        // GET: Barrios/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var barrio = _barrioRepositorio.GetFirst(p => p.Id == id, incluirPropiedades: "Localidad");
            if (barrio == null)
            {
                return NotFound();
            }

            return View(barrio);
        }


        // GET: Barrios/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var barrio = await _aplicacion.GetById(id);

            if (barrio == null)
            {
                return NotFound();
            }
            var localidades = _API_Localidad.Lista().Result.OrderBy(m => m.Nombre).ToList();
            ViewBag.Localidad_Id = new SelectList(localidades, "Id", "Nombre");
            return View(barrio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, BarrioCreacionDTO barrioCreacion)
        {
            if (id != barrioCreacion.Id)
            {
                return NotFound();
            }
            Barrio barrio = _mapper.Map<Barrio>(barrioCreacion);
            if (ModelState.IsValid)
            {
                _aplicacion.Updtae(barrio);
                return RedirectToAction(nameof(Index));
            }

            var localidades = _API_Localidad.Lista().Result.OrderBy(m => m.Nombre).ToList();
            ViewData["Localidad_Id"] = new SelectList(localidades, "Id", "Nombre");
            return View(barrio);
        }

        // GET: Barrios/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barrio = _barrioRepositorio.GetFirst(p => p.Id == id, incluirPropiedades: "Localidad");
            if (barrio == null)
            {
                return NotFound();
            }
            return View(barrio);
        }

        // POST: Barrios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var barrio = await _aplicacion.GetById(id);
            if (barrio != null)
            {
                _aplicacion.Remove(barrio);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
