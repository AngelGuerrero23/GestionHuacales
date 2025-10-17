using GestionHuacales.Api9.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionHuacales.Api9.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options): base(options){}

    public DbSet<EntradasHuacales> EntradasHuacales {  get; set; } 

    public DbSet<TiposHuacales> TiposHuacales { get;set; }

    public DbSet<EntradasHuacalesDetalles> EntradasHuacalesDetalles { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TiposHuacales>().HasData(
            new List<TiposHuacales>()
            {
                new()
                {
                    TipoId = 1,
                    Descripcion ="RojoGrande",
                },
                new()
                {
                    TipoId = 2,
                    Descripcion ="RojoMediano",
                },
                new()
                {
                    TipoId = 3,
                    Descripcion="VerdeGrande",
                },
                new()
                {   
                    TipoId = 4,
                    Descripcion="VerdeMediano"
                }
             
            }
        );
        base.OnModelCreating(modelBuilder);
    }
}
