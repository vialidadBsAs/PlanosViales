namespace Sistema_de_planos.Models
{
    public class PlanoModelGET
    {
        public int Id { get; set; }
        public int NumPlano { get; set; }
        public string Propietario { get; set; } = string.Empty;
        public double Arancel { get; set; }
        public DateTime FechaOriginal { get; set; }
        public string? EstadoDescripcion { get; set; }
        public string? PartidoNombre { get; set; }
        public string? PartidoInmobiliario { get; set; } = String.Empty;

        public DateTime? FechaRetiro { get; set; }
        public string Tipo { get; set; }
        public string? NombreRetiro { get; set; }
        public Boolean TieneHistoricos { get; set; }
        public int PartidoId { get; set; }

        public string PartidoIdNombre { get; set; }

        public string  EstadoCodDesc { get; set; }
    }
}
