using System.ComponentModel.DataAnnotations;

namespace IntelectahClinic.DTOs.Agendamento;
public class UpdateAgendamentoDTO
{
    [Required(ErrorMessage = "Id do agendamento é Obrigatório")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Nova Data do agendamento é Obrigatória")]
    public DateTime NovaDataHora { get; set; }
}