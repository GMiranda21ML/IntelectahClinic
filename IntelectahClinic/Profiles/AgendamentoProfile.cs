using AutoMapper;
using IntelectahClinic.DTOs.Agendamento;
using IntelectahClinic.DTOs.Especialidade;
using IntelectahClinic.DTOs.Paciente;
using IntelectahClinic.DTOs.Unidade;
using IntelectahClinic.Models;
using IntelectahClinic.Models.enums;

namespace IntelectahClinic.Profiles;


public class AgendamentoProfile : Profile
{
    public AgendamentoProfile()
    {
        CreateMap<CreateAgendamentoDTO, Agendamento>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => StatusAgendamento.AGENDADO));

        CreateMap<Agendamento, DadosAgendamentoBasicoDTO>()
            .ForMember(dest => dest.Especialidade, opt => opt.MapFrom(src => src.Especialidade))
            .ForMember(dest => dest.Unidade, opt => opt.MapFrom(src => src.Unidade));

        CreateMap<Agendamento, AgendamentoDetalhadoDTO>()
            .ForMember(dest => dest.Especialidade, opt => opt.MapFrom(src => src.Especialidade))
            .ForMember(dest => dest.Unidade, opt => opt.MapFrom(src => src.Unidade));

        CreateMap<Especialidade, DadosEspecialidadeDTO>();
        CreateMap<Unidade, DadosUnidadeDTO>();
    }
}

