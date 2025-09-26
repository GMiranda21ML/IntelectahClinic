using IntelectahClinic.DTOs.Agendamento;
using IntelectahClinic.Models;
using IntelectahClinic.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IntelectahClinic.Controllers;

[ApiController]
[Route("[controller]")]
public class AgendamentoController : ControllerBase
{
    private AgendamentoService _service;
    private UserManager<Paciente> _userManager;

    public AgendamentoController(AgendamentoService service, UserManager<Paciente> userManager)
    {
        _service = service;
        _userManager = userManager;
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
