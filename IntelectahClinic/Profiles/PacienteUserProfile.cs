using AutoMapper;
using IntelectahClinic.DTOs.User;
using IntelectahClinic.Models;

namespace IntelectahClinic.Profiles;

public class PacienteUserProfile : Profile
{
    public PacienteUserProfile()
    {
        CreateMap<CadastroPacienteDTO, Paciente>()
            .ForMember(p => p.Email, opt => opt.MapFrom(dto => dto.Email));
    }
}
