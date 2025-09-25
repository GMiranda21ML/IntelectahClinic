using IntelectahClinic.DTOs;
using IntelectahClinic.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IntelectahClinic.Controllers;

[ApiController]
[Route("user")]
public class PacienteUserController : Controller
{
    private PacienteUserService _userService;

    public PacienteUserController(PacienteUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("cadastro")] 
    public async Task<IActionResult> Cadastro([FromBody] CadastroPacienteDTO dto)
    {
        await _userService.Cadastro(dto);
        return Ok("Usuário Cadastrado!");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginPacienteDTO dto)
    {
        await _userService.Login(dto);
        return Ok("Usuário autenticado");
    }
}
