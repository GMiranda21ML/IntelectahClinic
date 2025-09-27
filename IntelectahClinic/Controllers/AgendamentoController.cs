using IntelectahClinic.DTOs.Agendamento;
using IntelectahClinic.Models;
using IntelectahClinic.Repository;
using IntelectahClinic.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

    [Authorize]
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

    [Authorize]
    [HttpPost("agendar")]
    public async Task<IActionResult> Agendar([FromBody] CreateAgendamentoDTO dto)
    {
        Paciente? paciente = await _userManager.GetUserAsync(User);
        var agendamento = _service.Agendar(dto, paciente.Id);

        return CreatedAtAction(nameof(BuscarAgendamentoPorId), new { id = agendamento.Id }, agendamento);
    }

    [Authorize]
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

    [Authorize]
    [HttpGet("meus-agendamentos")]
    public async Task<IActionResult> MeusAgendamentos()
    {
        Paciente? paciente = await _userManager.GetUserAsync(User);
        var agendamentos = await _service.ListarPorPaciente(paciente.Id);
        return Ok(agendamentos);
    }

    [Authorize]
    [HttpGet("proximos-agendamentos")]
    public async Task<IActionResult> ProximosAgendamentos()
    {
        Paciente? paciente = await _userManager.GetUserAsync(User);
        var agendamentos = await _service.ListarPorPacienteAgendado(paciente.Id);
        return Ok(agendamentos);
    }

    [Authorize]
    [HttpPost("cancelar/{id}")]
    public async Task<IActionResult> Cancelar(int id)
    {
        Paciente? paciente = await _userManager.GetUserAsync(User);
        await _service.Cancelar(id, paciente.Id);
        return Ok("Agendamento cancelado");
    }

    [Authorize]
    [HttpPost("reagendar")]
    public async Task<IActionResult> Reagendar([FromBody] UpdateAgendamentoDTO dto)
    {
        Paciente? paciente = await _userManager.GetUserAsync(User);
        await _service.Reagendar(dto, paciente.Id);
        return Ok("Agendamento reagendado");
    }

    [HttpGet("pdf-agendamento/{id}")]
    public async Task<IActionResult> GerarPdfAgendamentoPorId(int id)
    {
        var pdfBytes = await _service.GerarPdfAgendamentoPorId(id);
        return File(pdfBytes, "application/pdf", $"agendamento_{id}.pdf");
    }


}
