using IntelectahClinic.Service;
using Microsoft.AspNetCore.Mvc;

namespace IntelectahClinic.Controllers;

[ApiController]
[Route("[controller]")]
public class UnidadeController : ControllerBase
{
    private UnidadeService _service;

    public UnidadeController(UnidadeService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult BuscaUnidades()
    {
        return Ok(_service.BuscaUnidades());
    }
}
