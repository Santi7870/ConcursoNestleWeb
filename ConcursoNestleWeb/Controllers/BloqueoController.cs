using ConcursoNestleWeb.Data;
using ConcursoNestleWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

    // Acción para mostrar los datos registrados
    [HttpGet]
    public async Task<IActionResult> MostrarDatos(string searchName, bool? isBloqueo)
    {
        // Filtrar los registros según los parámetros de búsqueda
        var registros = _context.BloqueoDesbloqueo.AsQueryable();

        // Filtrar por nombre si se proporciona
        if (!string.IsNullOrEmpty(searchName))
        {
            registros = registros.Where(r => r.NombreEstudiante.Contains(searchName));
        }

        // Filtrar por tipo de bloqueo si se proporciona
        if (isBloqueo.HasValue)
        {
            registros = registros.Where(r => r.EsBloqueo == isBloqueo.Value);
        }

        // Obtener los resultados filtrados
        var resultado = await registros.ToListAsync();

        // Pasar los datos de búsqueda a la vista (el modelo ahora contiene los registros y los filtros)
        var model = new MostrarDatosViewModel
        {
            Registros = resultado,
            SearchName = searchName,
            IsBloqueo = isBloqueo
        };

        return View(model);
    }


}

