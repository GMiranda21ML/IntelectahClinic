using IntelectahClinic.Models.enums;
using System.ComponentModel.DataAnnotations;

namespace IntelectahClinic.Models;

public class Especialidade
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string NomeEspecialidade { get; set; }
    [Required]
    public TipoServico Tipo { get; set; }
}
