using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace dbTest.Models;

public partial class TestBgContext : DbContext
{
    public TestBgContext()
    {
    }

    public TestBgContext(DbContextOptions<TestBgContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Favorito> Favoritos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-PJEIDUL;Database=test_bg;Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__clientes__3213E83F091D1A2B");

            entity.ToTable("clientes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Favorito>(entity =>
        {
            entity.HasKey(e => new { e.ClienteId, e.ProductoId }).HasName("PK__favorito__9856838A074D3C9B");

            entity.ToTable("favoritos");

            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.ProductoId).HasColumnName("producto_id");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("estado");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Favoritos)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK__favoritos__clien__4AB81AF0");

            entity.HasOne(d => d.Producto).WithMany(p => p.Favoritos)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__favoritos__produ__4BAC3F29");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__producto__3213E83F0B5851ED");

            entity.ToTable("productos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.ImageSrc)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("imageSrc");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
