namespace Sistema_de_planos.Dominio.Entidades
{
    public partial class Partido
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public int? ZonaId { get; set; }
        public int? ZonaNro { get; set; }
    }
}
