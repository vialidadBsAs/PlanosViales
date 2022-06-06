﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Sistema_de_planos.Infraestructura.Datos
{
    public partial class PlanosContext : DbContext
    {
        public DbSet<Dominio.Entidades.Estado> Estados { get; set; }
        public DbSet<Dominio.Entidades.Partidos> Partidos { get; set; }
        public DbSet<Dominio.Entidades.Historico> Historicos { get; set; }
        public DbSet<Dominio.Entidades.Plano> Planos { get; set; }

        public PlanosContext()
        {

        }

        public PlanosContext(DbContextOptions<PlanosContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=SI-SOPORTE22\\SQLEXPRESS; Database=Planos; Integrated Security=true; Multiple Active Result Sets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}