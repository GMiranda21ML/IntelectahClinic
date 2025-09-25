using IntelectahClinic.DTOs;
using IntelectahClinic.Models.enums;
using IntelectahClinic.Repository;
using Microsoft.EntityFrameworkCore;

namespace IntelectahClinic.Service;

public class AgendamentoService
{
    private IntelectahClinicContext _context;

    public AgendamentoService(IntelectahClinicContext context)
    {
        _context = context;
    }

    public async Task<List<AgendamentoDTO>> ListarPorPaciente(string pacienteId)
    {
        return await _context.Agendamentos
            .Where(a => a.PacienteId == pacienteId)
            .OrderBy(a => a.DataHora)
            .Select(a => new AgendamentoDTO
            {
                Id = a.Id,
                Especialidade = a.Especialidade.NomeEspecialidade,
                Unidade = a.Unidade.NomeUnidade,
                DataHora = a.DataHora,
                Status = a.Status,
                Observacoes = a.Observacoes
            })
            .ToListAsync();
    }

    public async Task Cancelar(int agendamentoId, string pacienteId)
    {
        var agendamento = await _context.Agendamentos
            .FirstOrDefaultAsync(a => a.Id == agendamentoId && a.PacienteId == pacienteId);

        if (agendamento == null)
        {
            throw new ApplicationException("Agendamento não encontrado");
        }

        if (agendamento.Status == StatusAgendamento.ATENDIDO)
        {
            throw new ApplicationException("Paciente ja foi Atendido");
        }

        agendamento.Status = StatusAgendamento.CANCELADO;
        await _context.SaveChangesAsync();
    }


}
