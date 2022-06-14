namespace Sistema_de_planos.Models
{
    public class HistoricoModelGET
    {
        public int Id { get; set; }
        public string Observacion { get; set; } = string.Empty;
        public DateTime FechaPresentacion { get; set; }
        public DateTime FechaRetiro { get; set; }
        public string NombreRetiro { get; set; } = string.Empty;
        public string? EstadoDescripcion { get; set; }
    }
}