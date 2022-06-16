namespace Sistema_de_planos.Models
{
    public class PlanoModelPUT
    {
        public int Id { get;}
        public int NumPlano { get; set; }
        public string Propietario { get; set; } = string.Empty;
        public double Arancel { get; set; }
        public int EstadoId { get; set; }
        public int PartidoId { get; set; }
        public DateTime? FechaRetiro { get; set; }
        public string? NombreRetiro { get; set; }
    }
}
