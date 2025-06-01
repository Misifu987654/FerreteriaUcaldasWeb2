using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebFerreteria.Models;

public partial class FerreteriaDbContext : DbContext
{
    public FerreteriaDbContext()
    {
    }

    public FerreteriaDbContext(DbContextOptions<FerreteriaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Clientes> Clientes { get; set; }

    public virtual DbSet<DetallePedido> DetallePedido { get; set; }

    public virtual DbSet<Empresa> Empresa { get; set; }

    public virtual DbSet<Entregas> Entregas { get; set; }

    public virtual DbSet<Facturas> Facturas { get; set; }

    public virtual DbSet<Pedidos> Pedidos { get; set; }

    public virtual DbSet<Productos> Productos { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=EVA-01\\SQLSERVER;Database=BD_Ferreteria;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Clientes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clientes__3214EC07AA442B8A");

            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(50);
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DetalleP__3214EC07A0CD1572");

            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Pedido).WithMany(p => p.DetallePedido)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__DetallePe__Preci__440B1D61");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetallePedido)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__DetallePe__Produ__44FF419A");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empresa__3214EC0782AF5797");

            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(250);
            entity.Property(e => e.Nit).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(50);
        });

        modelBuilder.Entity<Entregas>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Entregas__3214EC07980AE9A6");

            entity.Property(e => e.DireccionEntrega).HasMaxLength(255);
            entity.Property(e => e.FechaEntrega).HasColumnType("datetime");

            entity.HasOne(d => d.EntregadoPorNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.EntregadoPor)
                .HasConstraintName("FK__Entregas__Entreg__4CA06362");

            entity.HasOne(d => d.Pedido).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.PedidoId)
                .HasConstraintName("FK__Entregas__Pedido__4BAC3F29");
        });

        modelBuilder.Entity<Facturas>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Facturas__3214EC07B54005B9");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Pedido).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.PedidoId)
                .HasConstraintName("FK__Facturas__Pedido__47DBAE45");
        });

        modelBuilder.Entity<Pedidos>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pedidos__3214EC0763346B18");

            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Pedidos__Estado__412EB0B6");
        });

        modelBuilder.Entity<Productos>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3214EC070AAE92C3");

            entity.Property(e => e.Descripcion).HasMaxLength(300);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC075F833F3A");

            entity.HasIndex(e => e.Correo, "UQ__Usuarios__60695A1967A966FA").IsUnique();

            entity.Property(e => e.Contrasena).HasMaxLength(255);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.NombreCompleto).HasMaxLength(100);
            entity.Property(e => e.Rol).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
