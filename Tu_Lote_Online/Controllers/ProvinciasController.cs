using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TuLote.Aplicacion;
using TuLote.Entidades;
using TuLote.Entidades.DTOs;
using TuLote.Servicios;

namespace TuLote.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ProvinciasController : Controller
    {

        private readonly IAplicacion<Provincia> _aplicacion;
        private readonly IServicio_API_Provincia _API_Provincia;
        private readonly IMapper _mapper;

        public ProvinciasController(IAplicacion<Provincia> aplicacion, IServicio_API_Provincia API_Provincia, IMapper mapper)
        {

            _aplicacion = aplicacion;
            _API_Provincia = API_Provincia;
            _mapper = mapper;
        }
        public async Task<ActionResult> Index()
        {
            var lista = await _aplicacion.GetAll();
            var listaMunicipios = _mapper.Map<List<ProvinciaDTO>>(lista);//Aquí se hace el mapeo
            return View(listaMunicipios);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.listaProvincias = new SelectList(await _API_Provincia.Lista(), "Id", "Nombre").OrderBy(p => p.Text);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProvinciaCreacionDTO provinciaCreacion)
        {

            ViewBag.provincias = "Ya se encuentra el registro";
            Provincia provincia = _mapper.Map<Provincia>(provinciaCreacion);//aquí se hace el mapeo
            var provinciasActuales = _aplicacion.GetAll(b => b.Provincias);
            if (ModelState.IsValid)
            {
                if (!provinciasActuales.Any(o => o.Nombre == provinciaCreacion.Nombre))
                {
                    await _aplicacion.Add(provincia);
                    TempData["success"] = "Registro Creado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.listaProvincias = new SelectList(await _API_Provincia.Lista(), "Id", "Nombre").OrderBy(p => p.Text);
            return View(provinciaCreacion);
        }


    }
}
