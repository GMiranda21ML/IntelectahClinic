using AutoMapper;
using AutoMapper.QueryableExtensions;
using IntelectahClinic.DTOs.Agendamento;
using IntelectahClinic.Models;
using IntelectahClinic.Models.enums;
using IntelectahClinic.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


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

        return horarios
            .Where(h => !agendados.Any(a =>
                a.Date == h.Date &&
                a.Hour == h.Hour &&
                a.Minute == h.Minute
            ))
            .ToList();
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

    public async Task<byte[]> GerarPdfAgendamentoPorId(int id)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var agendamento = await BuscarAgendamentoPorId(id);

        if (agendamento == null)
        {
            throw new ApplicationException("Agendamento não encontrado.");
        }

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Black));

                page.Header()
                    .PaddingBottom(10)
                    .BorderBottom(1)
                    .BorderColor(Colors.Grey.Lighten2)
                    .Row(row =>
                    {
                        row.RelativeItem()
                            .Text("Intelectah Clinic")
                            .Bold()
                            .FontSize(18)
                            .FontColor(Colors.Blue.Darken2);

                        row.RelativeItem()
                            .AlignRight()
                            .Text("Agendamento")
                            .FontSize(10)
                            .FontColor(Colors.Grey.Darken1);
                    });

                page.Content()
                    .PaddingVertical(20)
                    .Column(column =>
                    {
                        column.Item().Text($"Paciente: {agendamento.Paciente.NomeCompleto}").Bold();
                        column.Item().Text($"Especialidade: {agendamento.Especialidade.NomeEspecialidade}");
                        if (agendamento.Status == StatusAgendamento.AGENDADO)
                        {
                            column.Item().Text($"Status: {agendamento.Status}").FontColor(Colors.Green.Darken1);
                        }
                        else if (agendamento.Status == StatusAgendamento.CANCELADO)
                        {
                            column.Item().Text($"Status: {agendamento.Status}").FontColor(Colors.Red.Darken1);
                        }
                        else
                        {
                            column.Item().Text($"Status: {agendamento.Status}");
                        }
                        column.Item().Text($"Data: {agendamento.DataHora:dd/MM/yyyy}");
                        column.Item().Text($"Horário: {agendamento.DataHora:hh\\:mm}");
                        column.Item().Text($"Unidade: {agendamento.Unidade.NomeUnidade}");
                        column.Item().Text($"Endereço: {agendamento.Unidade.Endereco}");
                    });

                page.Footer()
                    .AlignCenter()
                    .Text($"Gerado em {DateTime.Now:dd/MM/yyyy HH:mm}")
                    .FontSize(10)
                    .FontColor(Colors.Grey.Darken1);
            });
        });

        return document.GeneratePdf();;
    }

}
