﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sistema_de_planos.Infraestructura.Datos;

#nullable disable

namespace Sistema_de_planos.Migrations
{
    [DbContext(typeof(PlanosContext))]
    [Migration("20220606160944_Cambioatributo")]
    partial class Cambioatributo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Sistema_de_planos.Dominio.Entidades.Estado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Estados");
                });

            modelBuilder.Entity("Sistema_de_planos.Dominio.Entidades.Historico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("EstadoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaPresentacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaRetiro")
                        .HasColumnType("datetime2");

                    b.Property<string>("NombreRetiro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlanoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.HasIndex("PlanoId");

                    b.ToTable("Historicos");
                });

            modelBuilder.Entity("Sistema_de_planos.Dominio.Entidades.Partidos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Partidos");
                });

            modelBuilder.Entity("Sistema_de_planos.Dominio.Entidades.Plano", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Arancel")
                        .HasColumnType("float");

                    b.Property<int>("EstadoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaOriginal")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumPlano")
                        .HasColumnType("int");

                    b.Property<int>("PartidoId")
                        .HasColumnType("int");

                    b.Property<string>("Propietario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.HasIndex("PartidoId");

                    b.ToTable("Planos");
                });

            modelBuilder.Entity("Sistema_de_planos.Dominio.Entidades.Historico", b =>
                {
                    b.HasOne("Sistema_de_planos.Dominio.Entidades.Estado", "Estado")
                        .WithMany("Historicos")
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sistema_de_planos.Dominio.Entidades.Plano", "Plano")
                        .WithMany("Historicos")
                        .HasForeignKey("PlanoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estado");

                    b.Navigation("Plano");
                });

            modelBuilder.Entity("Sistema_de_planos.Dominio.Entidades.Plano", b =>
                {
                    b.HasOne("Sistema_de_planos.Dominio.Entidades.Estado", "Estado")
                        .WithMany("Planos")
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sistema_de_planos.Dominio.Entidades.Partidos", "Partido")
                        .WithMany("Planos")
                        .HasForeignKey("PartidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estado");

                    b.Navigation("Partido");
                });

            modelBuilder.Entity("Sistema_de_planos.Dominio.Entidades.Estado", b =>
                {
                    b.Navigation("Historicos");

                    b.Navigation("Planos");
                });

            modelBuilder.Entity("Sistema_de_planos.Dominio.Entidades.Partidos", b =>
                {
                    b.Navigation("Planos");
                });

            modelBuilder.Entity("Sistema_de_planos.Dominio.Entidades.Plano", b =>
                {
                    b.Navigation("Historicos");
                });
#pragma warning restore 612, 618
        }
    }
}
