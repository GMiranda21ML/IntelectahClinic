using AutoMapper;
using IntelectahClinic.DTOs.Especialidade;
using IntelectahClinic.Models;

namespace IntelectahClinic.Profiles;

public class EspecialidadeProfile : Profile
{
    public EspecialidadeProfile()
    {
        CreateMap<Especialidade, DadosEspecialidadeDTO>();
    }
}
