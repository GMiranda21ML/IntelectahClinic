using IntelectahClinic.DTOs.Especialidade;
using IntelectahClinic.DTOs.Paciente;
using IntelectahClinic.DTOs.Unidade;
using IntelectahClinic.Models.enums;

namespace IntelectahClinic.DTOs.Agendamento;

public class DadosAgendamentoBasicoDTO
{
    public int Id { get; set; }

    public DadosEspecialidadeDTO Especialidade { get; set; }

    public DadosUnidadeDTO Unidade { get; set; }

    public DateTime DataHora { get; set; }

    public StatusAgendamento Status { get; set; }

    public string Observacoes { get; set; }
}
