namespace Sistema_de_planos.Dominio.Entidades
{
    public partial class Estado
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty ;

        public List<Plano> Planos { get; set; }
        public List<Historico> Historicos { get; set; } 
    }
}
