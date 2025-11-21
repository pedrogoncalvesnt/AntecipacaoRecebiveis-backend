using AntecipacaoRecebiveis.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AntecipacaoRecebiveis.Infrastructure;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Empresa> Empresas => Set<Empresa>();
    public DbSet<NotaFiscal> NotasFiscais => Set<NotaFiscal>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Cnpj).IsRequired().HasMaxLength(18);
            entity.Property(e => e.Nome).IsRequired();
            entity.Property(e => e.FaturamentoMensal).IsRequired().HasPrecision(18, 2);
            entity.Property(e => e.Ramo).IsRequired();

            entity.HasMany(e => e.NotasFiscais)
                  .WithOne(n => n.Empresa!)
                  .HasForeignKey(n => n.EmpresaId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<NotaFiscal>(entity =>
        {
            entity.HasKey(n => n.Id);
            entity.Property(n => n.Numero).IsRequired();
            entity.Property(n => n.Valor).IsRequired().HasPrecision(18,2);
            entity.Property(n => n.DataVencimento).IsRequired();
            entity.Property(n => n.Antecipada).IsRequired();

            entity.HasOne(n => n.Empresa)
                  .WithMany(e => e.NotasFiscais)
                  .HasForeignKey(n => n.EmpresaId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
