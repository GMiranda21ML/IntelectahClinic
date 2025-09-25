using AutoMapper;
using IntelectahClinic.DTOs;
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
    private UserContext _userContext;

    public PacienteUserService(IMapper mapper, UserManager<Paciente> userManager, UserContext userContext)
    {
        _mapper = mapper;
        _userManager = userManager;
        _userContext = userContext;
    }

    public async Task Cadastro(CadastroPacienteDTO dto)
    {
        if (_userContext.Pacientes.Any(p => p.Cpf == dto.Cpf))
        {
            throw new ApplicationException("Já existe um paciente com este CPF");
        } 

        Paciente paciente = _mapper.Map<Paciente>(dto);
        paciente.UserName = dto.NomeCompleto.Split(' ')[0];
        paciente.NomeCompleto = dto.NomeCompleto;
        IdentityResult resultado = await _userManager.CreateAsync(paciente, dto.Password);
    
        if (!resultado.Succeeded)
        {
            throw new ApplicationException("Falha ao Cadastrar Usuário!");
        }
    }
}
