using System;

namespace ConcursoNestleWeb.Models
{
    public class BloqueoDesbloqueo
    {
        public int Id { get; set; }
        public string NombreEstudiante { get; set; }
        public DateTime FechaHora { get; set; }
        public bool EsBloqueo { get; set; } // true para bloqueo, false para desbloqueo
    }
}

