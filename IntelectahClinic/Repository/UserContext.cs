using IntelectahClinic.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IntelectahClinic.Repository;

public class UserContext : IdentityDbContext<Paciente>
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
        
    }
}
