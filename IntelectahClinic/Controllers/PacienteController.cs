using AutoMapper;
using IntelectahClinic.DTOs.Paciente;
using IntelectahClinic.Models;
using IntelectahClinic.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IntelectahClinic.Controllers;

[ApiController]
[Route("[controller]")]
public class PacienteController : ControllerBase
{
    private PacienteService _pacienteService;
    private UserManager<Paciente> _userManager;

    public PacienteController(PacienteService pacienteService, UserManager<Paciente> userManager)
    {
        _pacienteService = pacienteService;
        _userManager = userManager;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> BuscarPacientePorId()
    {
        Paciente? paciente = await _userManager.GetUserAsync(User);
        DadosPacienteDTO pacienteDto = _pacienteService.BuscarPacientePorId(paciente.Id);

        
        if (pacienteDto == null)
        {
            return NotFound("Paciente não encontrado");
        }

        return Ok(pacienteDto);
    }
}
