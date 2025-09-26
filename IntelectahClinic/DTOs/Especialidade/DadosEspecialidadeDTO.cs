using IntelectahClinic.Models.enums;

namespace IntelectahClinic.DTOs.Especialidade;

public class DadosEspecialidadeDTO
{
    public int Id { get; set; }
    public string NomeEspecialidade { get; set; }
    public TipoServico Tipo { get; set; }

}
