using BoletoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BoletoAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<Boleto> Boletos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Banco>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.PercentualJuros).HasPrecision(3, 4);
            });

            modelBuilder.Entity<Boleto>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.CpfCnpjPagador).HasMaxLength(18);
                entity.Property(b => b.CpfCnpjBeneficiario).HasMaxLength(18);
                entity.Property(b => b.Valor).HasPrecision(13, 2);
                entity.Property(b => b.Observacao).HasMaxLength(500);
                entity.HasOne(b => b.Banco).WithMany().HasForeignKey(b => b.BancoId);
            });
        }

    }
}
