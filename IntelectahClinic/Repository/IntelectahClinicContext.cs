using IntelectahClinic.Models;
using Microsoft.EntityFrameworkCore;

namespace IntelectahClinic.Repository;

public class IntelectahClinicContext : DbContext
{
    //public DbSet<Paciente> Pacientes { get; set; }
    public IntelectahClinicContext(DbContextOptions<IntelectahClinicContext> options) : base(options) { }
}
