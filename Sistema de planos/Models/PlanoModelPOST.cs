namespace Sistema_de_planos.Models
{
    public class PlanoModelPOST
    {
        public int NumPlano { get; set; }
        public string Propietario { get; set; } = string.Empty;
        public double Arancel { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaOriginal { get; set; }
        public int EstadoId { get; set; }
        public int PartidoId { get; set; }
        public string? PartidoInmobiliario { get; set; } = String.Empty;

        public DateTime? FechaRetiro { get; set; }
        public string? NombreRetiro { get; set; }
        public string? Observaciones { get; set; }
        public string? Revisor { get; set; }
        public string? Prioridad { get; set; }

    }
}