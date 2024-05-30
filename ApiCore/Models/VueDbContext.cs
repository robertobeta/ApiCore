using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiCore.Models;

public partial class VueDbContext : DbContext
{
    public VueDbContext()
    {
    }

    public VueDbContext(DbContextOptions<VueDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Documentosplantilla> Documentosplantillas { get; set; }

    public virtual DbSet<Formulario> Formularios { get; set; }

    public virtual DbSet<Pruebsa> Pruebsas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=VueDB;user=root;password=antonio20019", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.1.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Documentosplantilla>(entity =>
        {
            entity.HasKey(e => e.IdDocumentosPlantilla).HasName("PRIMARY");

            entity.ToTable("documentosplantilla");

            entity.Property(e => e.IdDocumentosPlantilla).HasColumnName("idDocumentosPlantilla");
            entity.Property(e => e.DocumentosPlantillacol).HasMaxLength(45);
            entity.Property(e => e.Nombre).HasMaxLength(450);
            entity.Property(e => e.Tipo).HasMaxLength(450);
            entity.Property(e => e.Xml).HasColumnName("XML");
        });

        modelBuilder.Entity<Formulario>(entity =>
        {
            entity.HasKey(e => e.IdFormulario).HasName("PRIMARY");

            entity.ToTable("formulario");

            entity.Property(e => e.Apellidos)
                .HasMaxLength(40)
                .HasColumnName("apellidos");
            entity.Property(e => e.Comentario)
                .HasMaxLength(500)
                .HasColumnName("comentario");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(40)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Pruebsa>(entity =>
        {
            entity.HasKey(e => e.Idpruebsa).HasName("PRIMARY");

            entity.ToTable("pruebsa");

            entity.Property(e => e.Idpruebsa).HasColumnName("idpruebsa");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Activo).HasDefaultValueSql("'1'");
            entity.Property(e => e.Apellidos).HasMaxLength(45);
            entity.Property(e => e.Contrasena).HasMaxLength(45);
            entity.Property(e => e.Correo).HasMaxLength(45);
            entity.Property(e => e.Nombre).HasMaxLength(45);
            entity.Property(e => e.Telefono).HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
