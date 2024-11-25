using Microsoft.AspNetCore.Mvc;
using ConcursoNestleWeb.Data;
using ConcursoNestleWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcursoNestleWeb.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para registrar
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                // Verificar si ya existe el correo
                if (await _context.Usuarios.AnyAsync(u => u.Correo == usuario.Correo))
                {
                    ModelState.AddModelError("Correo", "El correo ya está registrado.");
                    return View(usuario);
                }

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            return View(usuario);
        }

        // Acción para mostrar el formulario de inicio de sesión
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Acción para procesar el inicio de sesión
        [HttpPost]
        public async Task<IActionResult> Login(string correo, string clave)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo && u.Clave == clave);

            if (usuario != null)
            {
                // Crear sesión
                HttpContext.Session.SetString("UsuarioId", usuario.Id.ToString());
                HttpContext.Session.SetString("NombreUsuario", usuario.Nombre);

                // Redirigir a la vista MostrarDatos
                return RedirectToAction("MostrarDatos", "Bloqueo");
            }

            ViewBag.Error = "Correo o clave incorrectos.";
            return View();
        }

        // Acción para cerrar sesión
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

