using System.ComponentModel.DataAnnotations;

namespace IntelectahClinic.DTOs.User;

public class CadastroPacienteDTO
{
    [Required(ErrorMessage = "Nome Completo é Obrigatório")]
    public string NomeCompleto { get; set; }
    [Required(ErrorMessage = "CPF é Obrigatório")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 dígitos")]
    public string Cpf {  get; set; }
    [Required(ErrorMessage = "Data de Nascimento é Obrigatório")]
    public DateTime DataNascimento { get; set; }
    [Required(ErrorMessage = "Número de Telefone é Obrigatório")]
    [DataType(DataType.PhoneNumber, ErrorMessage = "Número de Telefone Invalido")]
    public string Telefone { get; set; }
    [Required(ErrorMessage = "E-mail é Obrigatório")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required(ErrorMessage = "Convênio é Obrigatório")]
    [StringLength(100)]
    public string Convenio { get; set; }

    [Required(ErrorMessage = "Senha é Obrigatório")]
    [DataType(DataType.Password, ErrorMessage = "A senha precisa ter letras maiúsculas e menúsculas, número e caracter especial")]
    public string Password {  get; set; }
    [Required]
    [Compare("Password", ErrorMessage = "As senhas não são iguais")]
    public string PasswordConfirmada { get; set; }

}
