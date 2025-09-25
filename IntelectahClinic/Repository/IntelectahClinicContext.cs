using IntelectahClinic.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IntelectahClinic.Repository;

public class IntelectahClinicContext : IdentityDbContext<Paciente>
{
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Especialidade> Especialidades { get; set; }

    public IntelectahClinicContext(DbContextOptions<IntelectahClinicContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Paciente>()
            .HasIndex(p => p.Cpf)
            .IsUnique();

        builder.Entity<Especialidade>()
            .Property(e => e.Tipo)
            .HasConversion<string>();
    }
}
