using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IntelectahClinic.Models;

public class Paciente : IdentityUser
{
    [Required]
    public string Cpf { get; set; }
    [Required]
    public DateTime DataNascimento { get; set; }

    public Paciente() : base() { }
}
