namespace ConcursoNestleWeb.Models
{
    public class MostrarDatosViewModel
    {
        public IEnumerable<BloqueoDesbloqueo> Registros { get; set; }
        public string SearchName { get; set; }
        public bool? IsBloqueo { get; set; }
    }

}
