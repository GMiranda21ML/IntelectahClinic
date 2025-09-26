using AutoMapper;
using IntelectahClinic.DTOs.Paciente;
using IntelectahClinic.Models;

namespace IntelectahClinic.Profiles;

public class PacienteProfile : Profile
{
    public PacienteProfile()
    {
        CreateMap<Paciente, DadosPacienteDTO>();
    }
}
