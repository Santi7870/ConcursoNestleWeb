using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConcursoNestleWeb.Data;
using ConcursoNestleWeb.Models;

namespace ConcursoNestleWeb.Controllers
{
    // Asegúrate de que el controlador MVC esté configurado para recibir las solicitudes en la ruta correcta
    [Route("api/[controller]")]
    [ApiController]
    public class BloqueoDesbloqueoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BloqueoDesbloqueoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/BloqueoDesbloqueo/Registrar
        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarBloqueoDesbloqueo([FromBody] BloqueoDesbloqueoModel model)
        {
            if (model == null)
                return BadRequest("Datos inválidos.");

            var bloqueoDesbloqueo = new BloqueoDesbloqueo
            {
                NombreEstudiante = model.NombreEstudiante,
                EsBloqueo = model.EsBloqueo,
                FechaHora = DateTime.Now
            };

            _context.BloqueoDesbloqueo.Add(bloqueoDesbloqueo);
            await _context.SaveChangesAsync();

            return Ok("Registro exitoso.");
        }
    }

}
}


