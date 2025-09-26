using IntelectahClinic.Models.enums;
using System.ComponentModel.DataAnnotations;

namespace IntelectahClinic.Models;

public class Agendamento
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string PacienteId { get; set; }   
    public virtual Paciente Paciente { get; set; }
    [Required]
    public int EspecialidadeId { get; set; } 
    public virtual Especialidade Especialidade { get; set; }
    [Required]
    public int UnidadeId { get; set; }       
    public virtual Unidade Unidade { get; set; }
    [Required]
    public DateTime DataHora { get; set; }
    [Required]
    public StatusAgendamento Status { get; set; } 

    public string Observacoes { get; set; }

    public Agendamento() { }
}
