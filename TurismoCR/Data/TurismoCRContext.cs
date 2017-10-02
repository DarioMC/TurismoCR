using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TurismoCR.Models;

namespace TurismoCR.Data
{
    public class TurismoCRContext : DbContext
    {
        public TurismoCRContext(DbContextOptions<TurismoCRContext> options) : base(options)
        {
        }

        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<Reseña> Reseñas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orden>().ToTable("Orden");
            modelBuilder.Entity<Reseña>().ToTable("Resenia");
        }
    }
}
