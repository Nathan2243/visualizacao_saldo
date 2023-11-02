using Microsoft.EntityFrameworkCore;

namespace vizualizacao_saldo.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Saldo> Saldos { get; set; }
    }
}