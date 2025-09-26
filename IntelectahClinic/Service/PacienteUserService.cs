using AutoMapper;
using IntelectahClinic.DTOs.User;
using IntelectahClinic.Models;
using IntelectahClinic.Profiles;
using IntelectahClinic.Repository;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IntelectahClinic.Service;

public class PacienteUserService
{
    private IMapper _mapper;
    private UserManager<Paciente> _userManager;
    private IntelectahClinicContext _userContext;
    private SignInManager<Paciente> _signInManager;
    private TokenService _tokenService;

    public PacienteUserService(IMapper mapper, UserManager<Paciente> userManager, 
        IntelectahClinicContext userContext, SignInManager<Paciente> signInManager, TokenService tokenService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _userContext = userContext;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task Cadastro(CadastroPacienteDTO dto)
    {
        if (_userContext.Pacientes.Any(p => p.Cpf == dto.Cpf))
        {
            throw new ApplicationException("Já existe um paciente com este CPF");
        } 

        Paciente paciente = _mapper.Map<Paciente>(dto);
        paciente.UserName = dto.Cpf;
        IdentityResult resultado = await _userManager.CreateAsync(paciente, dto.Password);
    
        if (!resultado.Succeeded)
        {
            throw new ApplicationException("Falha ao Cadastrar Usuário!");
        }
    }

    public async Task<string> Login(LoginPacienteDTO dto)
    {
        var resultado = await _signInManager.PasswordSignInAsync(dto.Cpf, dto.Password, false, false);

        if (!resultado.Succeeded)
        {
            throw new ApplicationException("Usuário não autenticado!");
        }

        Paciente? usuario = _signInManager
            .UserManager
            .Users
            .FirstOrDefault(user => user.NormalizedUserName == dto.Cpf);

       string token =  _tokenService.GenerarToken(usuario);

       return token;
    }
}
