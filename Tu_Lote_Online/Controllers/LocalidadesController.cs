using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuLote.Aplicacion;
using TuLote.Entidades;
using TuLote.Entidades.DTOs;
using TuLote.Servicios;

namespace TuLote.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class LocalidadesController : Controller
    {
        private readonly IServicio_API_Localidad _API_Localidad;
        private readonly IServicio_API_Municipio _API_Municipio;
        private readonly Aplicacion.IAplicacion<Localidad> _aplicacion;
        private readonly IMapper _mapper;

        public LocalidadesController(IServicio_API_Localidad API_localidad, IServicio_API_Municipio API_Municipio,
            IAplicacion<Localidad> aplicacion, IMapper mapper)
        {
            _API_Localidad = API_localidad;
            _API_Municipio = API_Municipio;
            _aplicacion = aplicacion;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            //var lista = await _context.Localidades.Include(m => m.Municipio).ToListAsync();
            var lista = await _aplicacion.GetAll(l => l.localidades, m => m.Municipio).ToListAsync();
            var listaLocalidades = _mapper.Map<List<LocalidadDTO>>(lista);
            return View(listaLocalidades);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.listaMunicipios = _API_Municipio.Lista().Result.OrderBy(m => m.Nombre).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LocalidadCreacionDTO localidadCreacion)
        {
            var localidadesActuales = _aplicacion.GetAll(b => b.localidades);

            Localidad localidad = _mapper.Map<Localidad>(localidadCreacion);
            ViewBag.localidades = "Registro existente";
            if (ModelState.IsValid)
            {
                if (!localidadesActuales.Any(o => o.Nombre == localidadCreacion.Nombre))
                {
                    await _aplicacion.Add(localidad);
                    TempData["success"] = "Registro creado correctamente.";
                    return RedirectToAction(nameof(Index));
                }

            }
            ViewBag.listaMunicipios = _API_Municipio.Lista().Result.OrderBy(m => m.Nombre).ToList();
            return View(localidad);
        }




        public JsonResult GetLocalidadesById(int idMunicipio)
        {
            List<Localidad> localidades = new List<Localidad>();

            localidades = _API_Localidad.Lista().Result.Where(x => x.Municipio.Id == idMunicipio).OrderBy(l => l.Nombre).ToList();
            localidades.Insert(0, new Localidad
            {
                Id = 0,
                Nombre = "Por favor seleccione una localidad"
            });

            return Json(localidades);
        }

    }
}
