using IntelectahClinic.DTOs.Agendamento;
using IntelectahClinic.Migrations;
using IntelectahClinic.Models;
using IntelectahClinic.Repository;
using IntelectahClinic.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IntelectahClinic.Controllers;

[ApiController]
[Route("[controller]")]
public class AgendamentoController : ControllerBase
{
    private AgendamentoService _service;
    private UserManager<Paciente> _userManager;
    private IntelectahClinicContext _context;

    public AgendamentoController(AgendamentoService service, UserManager<Paciente> userManager, IntelectahClinicContext context)
    {
        _service = service;
        _userManager = userManager;
        _context = context;
    }

    [HttpGet("disponibilidade")]
    public async Task<IActionResult> GetDisponibilidade(int unidadeId, int especialidadeId, DateTime data)
    {
        IEnumerable<DateTime> horarios = await _service.GetDisponibilidade(unidadeId, especialidadeId, data);

        if (horarios == null || horarios.Count() == 0)
        {
            return NoContent();
        }

        return Ok(horarios);
        
    }

    [HttpPost("agendar")]
    public IActionResult Agendar([FromBody] AgendamentoDTO dto)
    {
        var agendamento = _service.Agendar(dto);

        return CreatedAtAction(nameof(BuscarAgendamentoPorId), new { id = agendamento.Id }, agendamento);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarAgendamentoPorId(int id)
    {
        var agendamento = await _service.BuscarAgendamentoPorId(id);

        if (agendamento == null)
        {
            return NotFound();
        }

        return Ok(agendamento);
    }


    [HttpGet("meus-agendamentos/{pacienteId}")]
    public async Task<IActionResult> MeusAgendamentos(string pacienteId)
    {
        var agendamentos = await _service.ListarPorPaciente(pacienteId);
        return Ok(agendamentos);
    }

    [HttpPost("cancelar/{id}/{pacienteId}")]
    public async Task<IActionResult> Cancelar(int id, string pacienteId)
    {
        await _service.Cancelar(id, pacienteId);
        return Ok("Agendamento cancelado");
    }

    [HttpPost("reagendar/{pacienteId}")]
    public async Task<IActionResult> Reagendar([FromBody] AtualizarAgendamentoDTO dto, string pacienteId)
    {;
        await _service.Reagendar(dto, pacienteId);
        return Ok("Agendamento reagendado");
    }
}
