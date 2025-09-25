using AutoMapper;
using IntelectahClinic.DTOs;
using IntelectahClinic.Models;
using IntelectahClinic.Profiles;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IntelectahClinic.Service;

public class PacienteUserService
{
    private IMapper _mapper;
    private UserManager<Paciente> _userManager;

    public PacienteUserService(IMapper mapper, UserManager<Paciente> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task Cadastro(CadastroPacienteDTO dto)
    {
        Paciente paciente = _mapper.Map<Paciente>(dto);
        paciente.UserName = dto.NomeCompleto.Split(' ')[0];
        paciente.NomeCompleto = dto.NomeCompleto;
        IdentityResult resultado = await _userManager.CreateAsync(paciente, dto.Password);
    
        if (!resultado.Succeeded)
        {
            var erros = string.Join(" | ", resultado.Errors.Select(e => e.Description));
            throw new ApplicationException($"Falha ao Cadastrar Usuário: {erros}");
        }
    }
}
