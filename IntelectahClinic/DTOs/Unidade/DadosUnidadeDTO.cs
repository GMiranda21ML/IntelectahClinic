using System.ComponentModel.DataAnnotations;

namespace IntelectahClinic.DTOs.Unidade;

public class DadosUnidadeDTO
{
    public int Id { get; set; }
    public string NomeUnidade { get; set; }
    public string Endereco { get; set; }
}
