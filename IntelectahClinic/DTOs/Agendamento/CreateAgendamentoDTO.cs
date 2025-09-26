using IntelectahClinic.Models.enums;
using System.ComponentModel.DataAnnotations;

namespace IntelectahClinic.DTOs.Agendamento;

public class CreateAgendamentoDTO
{
    [Required(ErrorMessage = "O Id da especialidade é obrigatório")]
    public int EspecialidadeId { get; set; }
    [Required(ErrorMessage = "O Id da unidade é obrigatório")]
    public int UnidadeId { get; set; }
    [Required(ErrorMessage = "A Data é obrigatória")]
    public DateTime DataHora { get; set; }
    [Required(ErrorMessage = "O Status do agendamento é obrigatório")]
    public StatusAgendamento Status { get; set; }

    public string Observacoes { get; set; }
}
