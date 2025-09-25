using Microsoft.AspNetCore.Mvc;

namespace IntelectahClinic.Controllers;

[ApiController]
[Route("[controller]")]
public class TesteController : ControllerBase
{
    [HttpGet]
    public IActionResult teste()
    {
        return Ok("Tudo Funcionando!");
    }
}
