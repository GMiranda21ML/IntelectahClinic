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

    [HttpGet("meus-agendamentos/{id}")]
    public async Task<IActionResult> MeusAgendamentos(string id)
    {
        var agendamentos = await _service.ListarPorPaciente(id);
        return Ok(agendamentos);
    }

    [HttpPost("cancelar/{id}/{pacienteId}")]
    public async Task<IActionResult> Cancelar(int id, string pacienteId)
    {
        await _service.Cancelar(id, pacienteId);
        return Ok("Agendamento cancelado");
    }
}
