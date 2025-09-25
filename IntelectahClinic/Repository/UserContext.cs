using IntelectahClinic.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IntelectahClinic.Repository;

public class UserContext : IdentityDbContext<Paciente>
{
    public DbSet<Paciente> Pacientes { get; set; }

    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Paciente>()
            .HasIndex(p => p.Cpf)
            .IsUnique();
    }
}
