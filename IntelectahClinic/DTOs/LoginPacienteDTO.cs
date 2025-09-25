using System.ComponentModel.DataAnnotations;

namespace IntelectahClinic.DTOs;

public class LoginPacienteDTO
{
    [Required(ErrorMessage = "CPF é Obrigatório")]
    public string Cpf { get; set; }
    [Required(ErrorMessage = "Senha é Obrigatória")]
    public string Password { get; set; }
}
