using Microsoft.EntityFrameworkCore;
using FrutasDoSeuZe.Models;

namespace FrutasDoSeuZe.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Fruta> Frutas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // substitua o "postgres" e a senha conforme seu setup
            optionsBuilder.UseNpgsql("Host=localhost;Database=frutas_db;Username=postgres;Password=JanGustavo083#");
        }
    }
}
