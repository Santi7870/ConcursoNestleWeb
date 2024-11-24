using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConcursoNestleWeb.Data;
using ConcursoNestleWeb.Models;

namespace ConcursoNestleWeb.Controllers
{
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
        [HttpPost]
        public async Task<IActionResult> RegistrarBloqueoDesbloqueo([FromBody] BloqueoDesbloqueoModel model)
        {
            if (!ModelState.IsValid)  // Validación del modelo
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Crear la entidad para registrar el bloqueo/desbloqueo
                var bloqueoDesbloqueo = new BloqueoDesbloqueo
                {
                    NombreEstudiante = model.NombreEstudiante,
                    EsBloqueo = model.EsBloqueo,
                    FechaHora = DateTime.Now
                };

                // Guardar en la base de datos
                _context.BloqueoDesbloqueo.Add(bloqueoDesbloqueo);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Registro exitoso", bloqueoDesbloqueo });
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, $"Error al guardar los datos: {ex.Message}");
            }
        }
    }
}


