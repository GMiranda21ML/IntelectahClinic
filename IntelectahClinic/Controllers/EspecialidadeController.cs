using IntelectahClinic.Models.enums;
using IntelectahClinic.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntelectahClinic.Controllers;

[ApiController]
[Route("[controller]")]
public class EspecialidadeController : ControllerBase
{

    private EspecialidadeService _service;

    public EspecialidadeController(EspecialidadeService service)
    {
        _service = service;
    }

    [HttpGet("tipos-servico")]
    public IActionResult GetTipoServico()
    {
        return Ok(_service.GetTipoServico());
    }

    [HttpGet]
    public async Task<IActionResult> GetEspecialidades([FromQuery] TipoServico? tipo)
    {

        return Ok(_service.GetEspecialidades(tipo));
    }
}
