namespace Sistema_de_planos.Models
{
    public class HistoricoModelPOST
    {
        public int PlanoId { get; set; }
        public int EstadoId { get; set; }
        public string Observacion { get; set; } = string.Empty;
        public DateTime FechaRetiro { get; set; }
        public string NombreRetiro { get; set; } = string.Empty;
    }
}
