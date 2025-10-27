using Microsoft.EntityFrameworkCore;
using FrutasDoSeuZe.Models;

namespace FrutasDoSeuZe.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Fruta> Frutas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=FrutasDoSeuZe;Username=postgres;Password=123");
            }
        }

    }
}
