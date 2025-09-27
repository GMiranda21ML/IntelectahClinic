using AutoMapper;
using AutoMapper.QueryableExtensions;
using IntelectahClinic.DTOs.Agendamento;
using IntelectahClinic.DTOs.Especialidade;
using IntelectahClinic.DTOs.Paciente;
using IntelectahClinic.DTOs.Unidade;
using IntelectahClinic.Models;
using IntelectahClinic.Models.enums;
using IntelectahClinic.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Threading.Tasks;

namespace IntelectahClinic.Service;

public class AgendamentoService
{
    private IntelectahClinicContext _context;
    private IMapper _mapper;

    public AgendamentoService(IntelectahClinicContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DateTime>> GetDisponibilidade(int unidadeId, int especialidadeId, DateTime data)
    {
        if (data < DateTime.Now)
        {
            return new List<DateTime>();
        }

        if (data.DayOfWeek == DayOfWeek.Saturday || data.DayOfWeek == DayOfWeek.Sunday)
        {
            return new List<DateTime>();
        }

        var horarios = Enumerable.Range(8, 10)
            .Select(h => new DateTime(data.Year, data.Month, data.Day, h, 0, 0))
            .ToList();

        var agendados = await _context.Agendamentos
            .Where(a => a.UnidadeId == unidadeId
                     && a.EspecialidadeId == especialidadeId
                     && a.DataHora.Date == data.Date)
            .Select(a => a.DataHora)
            .ToListAsync();

        return horarios.Where(h => !agendados.Contains(h)).ToList();

    }

    public Agendamento Agendar([FromBody] CreateAgendamentoDTO dto, string pacienteId)
    {
        var agendamento = _mapper.Map<Agendamento>(dto);
        agendamento.PacienteId = pacienteId;
        _context.Agendamentos.Add(agendamento);
        _context.SaveChanges();
        return agendamento;

    }

    public async Task<List<DadosAgendamentoBasicoDTO>> ListarPorPaciente(string pacienteId)
    {
        return await _context.Agendamentos
            .Where(a => a.PacienteId == pacienteId)
            .Include(a => a.Especialidade)
            .Include(a => a.Unidade)
            .ProjectTo<DadosAgendamentoBasicoDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<List<DadosAgendamentoBasicoDTO>> ListarPorPacienteAgendado(string pacienteId)
    {
        return await _context.Agendamentos
            .Where(a => a.PacienteId == pacienteId && a.Status == StatusAgendamento.AGENDADO)
            .Include(a => a.Especialidade)
            .Include(a => a.Unidade)
            .ProjectTo<DadosAgendamentoBasicoDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<AgendamentoDetalhadoDTO> BuscarAgendamentoPorId(int id)
    {
        return await _context.Agendamentos
            .Where(a => a.Id == id)
            .Include(a => a.Especialidade)
            .Include(a => a.Unidade)
            .ProjectTo<AgendamentoDetalhadoDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
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

    public async Task Reagendar(UpdateAgendamentoDTO dto, string pacienteId)
    {
        var agendamento = await _context.Agendamentos
            .FirstOrDefaultAsync(a => a.Id == dto.Id && a.PacienteId == pacienteId);

        if (agendamento == null)
        {
            throw new ApplicationException("Agendamento não encontrado");
        }

        if (agendamento.Status == StatusAgendamento.ATENDIDO)
        {
            throw new ApplicationException("Paciente ja foi Atendido");
        }

        agendamento.DataHora = dto.NovaDataHora;
        agendamento.Status = StatusAgendamento.AGENDADO;
        await _context.SaveChangesAsync();
    }

}
