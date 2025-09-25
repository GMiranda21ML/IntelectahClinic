using AutoMapper;
using IntelectahClinic.DTOs;
using IntelectahClinic.Models;
using IntelectahClinic.Repository;

namespace IntelectahClinic.Service;

public class PacienteService
{
    private UserContext _context;
    private IMapper _mapper;

    public PacienteService(UserContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public DadosPacienteDTO BuscarPacientePorId(string id)
    {
        return _mapper.Map<DadosPacienteDTO>(_context.Pacientes.FirstOrDefault(p => p.Id.Equals(id)));
    }
}
