using IntelectahClinic.DTOs;
using IntelectahClinic.Service;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> CadastroAsync([FromBody] CadastroPacienteDTO dto)
    {
        await _userService.Cadastro(dto);
        return Ok("Usuário Cadastrado!");
    }


}
