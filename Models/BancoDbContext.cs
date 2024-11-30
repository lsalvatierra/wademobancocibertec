using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace wademobancocibertec.Models;

public partial class BancoDbContext : DbContext
{
    public BancoDbContext()
    {
    }

    public BancoDbContext(DbContextOptions<BancoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cuenta> Cuenta { get; set; }

    public virtual DbSet<Transaccion> Transaccion { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cuenta__3214EC07D094E65C");

            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Saldo).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transacc__3214EC0728B1E30F");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CuentaDestino).WithMany(p => p.TransaccionCuentaDestino)
                .HasForeignKey(d => d.CuentaDestinoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacci__Cuent__3A81B327");

            entity.HasOne(d => d.CuentaOrigen).WithMany(p => p.TransaccionCuentaOrigen)
                .HasForeignKey(d => d.CuentaOrigenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacci__Cuent__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
