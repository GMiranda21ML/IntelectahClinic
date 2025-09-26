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
        CreateMap<AgendamentoDTO, Agendamento>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => StatusAgendamento.AGENDADO));
        CreateMap<Agendamento, AgendamentoDetalhadoDTO>();
        CreateMap<Paciente, DadosPacienteDTO>();
        CreateMap<Especialidade, DadosEspecialidadeDTO>();
        CreateMap<Unidade, DadosUnidadeDTO>();
    }
}
