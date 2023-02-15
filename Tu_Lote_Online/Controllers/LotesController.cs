using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TuLote.Abstracciones;
using TuLote.Aplicacion;
using TuLote.Entidades;
using TuLote.Entidades.DTOs;
using TuLote.Repositorio;

namespace TuLote.Controllers
{
    public class LotesController : Controller
    {

        private readonly IAplicacion<Lote> _aplicacion;
        private readonly IDbContext<Barrio> _barrio;
        private readonly IRepositorio<Usuario> _usuario;
        private readonly IMapper _mapper;
        private readonly ILoteRepository<Lote> _loteRepository;

        public LotesController(IAplicacion<Lote> aplicacion, IDbContext<Barrio> barrio,
            IRepositorio<Usuario> usuario, IMapper mapper, ILoteRepository<Lote> loteRepository)
        {
            _aplicacion = aplicacion;
            _barrio = barrio;
            _usuario = usuario;
            _mapper = mapper;
            _loteRepository = loteRepository;
        }
        public async Task<IActionResult> Index()
        {
            var lista = await _aplicacion.GetAll(b => b.Barrio, l => l.Barrio.Localidad, l => l.Usuario).ToListAsync();
            var listaLotes = _mapper.Map<List<LoteDTO>>(lista);//Aquí se hace el mapeo

            return View(listaLotes);
        }

        //Get
        public async Task<IActionResult> Crear()
        {
            var barrios = await _barrio.GetAll();
            var usuarios = await _usuario.GetAll();
            ViewData["Barrio_Id"] = new SelectList(barrios, "Id", "Nombre");
            ViewData["Usuario"] = new SelectList(usuarios, "Id", "Alias");
            return View();
        }

        //Post
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Crear(LoteCreacionDTO loteCreacion)
        {

            Lote lote = _mapper.Map<Lote>(loteCreacion);
            if (ModelState.IsValid)
            {
                await _aplicacion.Add(lote);
                TempData["success"] = "Registro creado correctamente.";
                return RedirectToAction("Index");
            }
            var barrios = await _barrio.GetAll();
            var usuarios = await _usuario.GetAll();
            ViewData["Barrio_Id"] = new SelectList(barrios, "Id", "Nombre");
            ViewData["Usuario"] = new SelectList(usuarios, "Id", "Alias");
            return (View(lote));
        }

        public async Task<IActionResult> Details(int id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var lote = _loteRepository.GetFirst(p => p.Id == id, incluirPropiedades: "Barrio");
            if (lote == null)
            {
                return NotFound();
            }

            return View(lote);
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lote = _loteRepository.GetFirst(p => p.Id == id, incluirPropiedades: "Barrio");
            var usuarios = await _usuario.GetAll();
            var barrios = await _barrio.GetAll();
            var usuario = _loteRepository.GetFirst(p => p.Id == id, incluirPropiedades: "Usuario");
            if (lote == null)
            {
                return NotFound();
            }

            ViewData["Barrio_Id"] = new SelectList(barrios, "Id", "Nombre");
            ViewData["Usuario"] = new SelectList(usuarios, "Id", "Alias");
            //ViewData["Usuario"] = new SelectList(_context.Users, "Id", "Alias");
            return View(lote);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, LoteCreacionDTO loteCreacion)
        {
            if (id != loteCreacion.Id)
            {
                return NotFound();
            }

            Lote lote = _mapper.Map<Lote>(loteCreacion);
            var barrios = await _barrio.GetAll();
            if (ModelState.IsValid)
            {
                try
                {
                    _aplicacion.Updtae(lote);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }


                return RedirectToAction(nameof(Index));
            }
            ViewData["Barrio_Id"] = new SelectList(barrios, "Id", "Nombre");
            // ViewData["Barrio_Id"] = new SelectList(_context.Barrios, "Id", "Nombre", lote.Barrio_Id);
            return View(lote);
        }

        // GET: Barrios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lote = _loteRepository.GetFirst(p => p.Id == id, incluirPropiedades: "Barrio");

            if (lote == null)
            {
                return NotFound();
            }

            return View(lote);
        }

        // POST: Barrios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var lote = _loteRepository.GetFirst(p => p.Id == id, incluirPropiedades: "Barrio");
            var lote = await _aplicacion.GetById(id);
            if (lote == null)
            {
                return Problem("El lote solicitado no existe");
            }

            if (lote != null)
            {
                _aplicacion.Remove(lote);

            }
            return RedirectToAction(nameof(Index));
        }


    }
}
