using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TuLote.Entidades;

namespace TuLote.AccesoDatos
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Localidad> Localidades { get; set; }
        public DbSet<Barrio> Barrios { get; set; }
        public DbSet<Lote> Lotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Provincia>()
        .Property(p => p.Id)
        .ValueGeneratedNever();

            modelBuilder.Entity<Municipio>()
        .Property(p => p.Id)
        .ValueGeneratedNever();

            modelBuilder.Entity<Localidad>()
        .Property(p => p.Id)
        .ValueGeneratedNever();

            base.OnModelCreating(modelBuilder);

        }
    }
}
