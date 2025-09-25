using IntelectahClinic.DTOs;
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

}
