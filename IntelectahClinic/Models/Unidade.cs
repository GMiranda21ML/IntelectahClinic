using System.ComponentModel.DataAnnotations;

namespace IntelectahClinic.Models;

public class Unidade
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string NomeUnidade { get; set; }
    [Required]
    public string Endereco { get; set; }
}
