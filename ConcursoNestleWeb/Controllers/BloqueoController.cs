using ConcursoNestleWeb.Data;
using ConcursoNestleWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class BloqueoController : Controller
{
    private readonly ApplicationDbContext _context;

    // Inyección del DbContext para acceder a la base de datos
    public BloqueoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Acción para registrar el bloqueo o desbloqueo
    [HttpPost]
    public async Task<IActionResult> Registrar([FromBody] BloqueoDesbloqueoModel model)
    {
        if (ModelState.IsValid)
        {
            // Aquí guardas el dato en la base de datos
            var registro = new BloqueoDesbloqueo
            {
                NombreEstudiante = model.NombreEstudiante,
                EsBloqueo = model.EsBloqueo,
                FechaHora = DateTime.Now
            };

            // Agregar el registro a la base de datos
            _context.BloqueoDesbloqueo.Add(registro);
            await _context.SaveChangesAsync();

            return Ok(); // Retornar OK si se guarda correctamente
        }

        return BadRequest(); // Retornar BadRequest si hay un error en el modelo
    }
}

