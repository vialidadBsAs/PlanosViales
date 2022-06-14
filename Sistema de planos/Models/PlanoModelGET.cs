﻿namespace Sistema_de_planos.Models
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
    }
}