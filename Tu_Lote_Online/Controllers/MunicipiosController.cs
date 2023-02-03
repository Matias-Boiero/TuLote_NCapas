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
    public class MunicipiosController : Controller
    {

        private readonly IAplicacion<Municipio> _aplicacion;
        private readonly IServicio_API_Provincia _API_Provincia;
        private readonly IServicio_API_Municipio _API_Municipio;
        private readonly IMapper _mapper;

        public MunicipiosController(IAplicacion<Municipio> aplicacion, IServicio_API_Provincia API_Provincia,
            IServicio_API_Municipio API_Municipio, IMapper mapper)
        {

            _aplicacion = aplicacion;
            _API_Provincia = API_Provincia;
            _API_Municipio = API_Municipio;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var lista = await _aplicacion.GetAll(m => m.Municipios, p => p.Provincia).ToListAsync();
            var listaMunicipios = _mapper.Map<List<MunicipioDTO>>(lista);//Aquí se hace el mapeo
            return View(listaMunicipios);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Provincias = _API_Provincia.Lista().Result.OrderBy(p => p.Nombre);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MunicipioCreacionDTO municipioCreacion)
        {
            var municipiosActuales = _aplicacion.GetAll(b => b.Municipios);
            ViewBag.municipios = "Ya se encuentra el registro";
            var municipio = _mapper.Map<Municipio>(municipioCreacion);
            if (ModelState.IsValid)
            {
                if (!municipiosActuales.Any(o => o.Nombre == municipioCreacion.Nombre))
                {
                    await _aplicacion.Add(municipio);
                    TempData["success"] = "Registro Creado correctamente.";
                    return RedirectToAction(nameof(Index));
                }

            }
            ViewBag.Provincias = _API_Provincia.Lista().Result.OrderBy(p => p.Nombre);
            return View(municipio);
        }


        public JsonResult GetMunicipiosById(int idProvincia)
        {
            List<Municipio> municipios = new List<Municipio>();

            municipios = _API_Municipio.Lista().Result.Where(x => x.Provincia.Id == idProvincia).OrderBy(x => x.Nombre).ToList();
            municipios.Insert(0, new Municipio
            {
                Id = 0,
                Nombre = "Por favor seleccione un municipio"
            });
            return Json(municipios);
        }


    }
}
