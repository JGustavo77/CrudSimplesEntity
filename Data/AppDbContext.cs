﻿using Microsoft.EntityFrameworkCore;
using FrutasDoSeuZe.Models;
using Microsoft.Extensions.Configuration;


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
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=frutas_db;Username=postgres;Password=JanGustavo083#");
            }
        }

    }
}
