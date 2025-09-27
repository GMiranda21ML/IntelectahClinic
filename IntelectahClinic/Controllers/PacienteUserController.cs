using IntelectahClinic.DTOs.User;
using IntelectahClinic.Service;
using Microsoft.AspNetCore.Authorization;
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

    [AllowAnonymous]
    [HttpPost("cadastro")] 
    public async Task<IActionResult> Cadastro([FromBody] CadastroPacienteDTO dto)
    {
        await _userService.Cadastro(dto);
        return Ok("Usuário Cadastrado!");
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginPacienteDTO dto)
    {
        await _userService.Login(dto);
        return Ok("Usuário autenticado");
    }
}
