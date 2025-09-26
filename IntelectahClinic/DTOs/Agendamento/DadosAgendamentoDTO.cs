using IntelectahClinic.Models.enums;

namespace IntelectahClinic.DTOs.Agendamento;

public class DadosAgendamentoDTO
{
    public int Id { get; set; }
    public string Especialidade { get; set; }
    public string Unidade { get; set; }
    public DateTime DataHora { get; set; }
    public StatusAgendamento Status { get; set; }
    public string Observacoes { get; set; }
}
