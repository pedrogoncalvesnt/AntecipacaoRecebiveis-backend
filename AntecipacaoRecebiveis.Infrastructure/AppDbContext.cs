using AntecipacaoRecebiveis.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AntecipacaoRecebiveis.Infrastructure
{
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
                entity.Property(e => e.FaturamentoMensal).IsRequired();
                entity.Property(e => e.Ramo).IsRequired();
            });

            modelBuilder.Entity<NotaFiscal>(entity =>
            {
                entity.HasKey(n => n.Id);
                entity.Property(n => n.NumeroNotaFiscal).IsRequired();
                entity.Property(n => n.Valor).IsRequired();
                entity.Property(n => n.DataVencimento).IsRequired();

                entity.HasOne(n => n.Empresa)
                      .WithMany()
                      .HasForeignKey(n => n.EmpresaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
