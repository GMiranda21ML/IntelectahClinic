using AutoMapper;
using IntelectahClinic.DTOs;
using IntelectahClinic.Models;
using IntelectahClinic.Service;
using Microsoft.AspNetCore.Mvc;

namespace IntelectahClinic.Controllers;

[ApiController]
[Route("[controller]")]
public class PacienteController : ControllerBase
{
    private PacienteService _pacienteService;

    public PacienteController(PacienteService pacienteService)
    {
        _pacienteService = pacienteService;
    }

    [HttpGet("{id}")]
    public IActionResult BuscarPacientePorId(string id)
    {
        DadosPacienteDTO pacienteDto = _pacienteService.BuscarPacientePorId(id);

        
        if (pacienteDto == null)
        {
            return NotFound("Paciente não encontrado");
        }

        return Ok(pacienteDto);
    }
}
