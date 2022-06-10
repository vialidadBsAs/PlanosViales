namespace Sistema_de_planos.Models
{
    public class PlanoModelPOST
    {
        public int NumPlano { get; set; }
        public string Propietario { get; set; } = string.Empty;
        public double Arancel { get; set; }
        public DateTime FechaOriginal { get; set; }
        public int EstadoId { get; set; }
        public int PartidoId { get; set; }
    }
}
