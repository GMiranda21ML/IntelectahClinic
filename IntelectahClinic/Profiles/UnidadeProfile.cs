using AutoMapper;
using IntelectahClinic.DTOs.Unidade;
using IntelectahClinic.Models;

namespace IntelectahClinic.Profiles;

public class UnidadeProfile : Profile
{
    public UnidadeProfile()
    {
        CreateMap<Unidade, DadosUnidadeDTO>();
    }
}
