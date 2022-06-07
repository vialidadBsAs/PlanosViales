namespace Sistema_de_planos.Dominio.Entidades
{
    public partial class Historico
    {
        public int Id { get; set; }
        public string Observacion { get; set; } = string.Empty;
        public DateTime FechaPresentacion { get; set; }
        public DateTime FechaRetiro { get; set; }
        public string NombreRetiro { get; set; } = string.Empty;
        public Plano? Plano { get; set; }
        public Estado? Estado { get; set; }
        public int PlanoId { get; set; }
        public int EstadoId { get; set; }
    }
}
