using AutoMapper;
using IntelectahClinic.DTOs;
using IntelectahClinic.Models;

namespace IntelectahClinic.Profiles;

public class PacienteProfile : Profile
{
    public PacienteProfile()
    {
        CreateMap<Paciente, DadosPacienteDTO>();
    }
}
