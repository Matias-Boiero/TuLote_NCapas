using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TuLote.Entidades;
using TuLote.Models.ViewModel;

namespace TuLote.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UserManager<Usuario> _user;
        private readonly SignInManager<Usuario> _signIn;

        public UsuariosController(UserManager<Usuario> user, SignInManager<Usuario> signIn)
        {
            _user = user;
            _signIn = signIn;
        }

        [AllowAnonymous]
        public ActionResult Registro()
        {
            return View();
        }

        // POST: UsuariosController/registro
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registro(RegistroVM model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var usuario = new Usuario()
            {
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                Alias = model.Alias,
                Email = model.Email,
                UserName = model.Email,
            };

            var resultado = await _user.CreateAsync(usuario, password: model.Contraseña);
            if (resultado.Succeeded)
            {
                await _signIn.SignInAsync(usuario, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }
                return View(model);
            }
        }

        // GET
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        // POST: UsuariosController/login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginVM model)
        {
            //Si no es valido
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var resultado = await _signIn.PasswordSignInAsync(model.Email, model.Password, model.Recuerdame, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Password", "Email o contraseña incorrectos");
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Login", "Usuarios");
        }
    }
}
