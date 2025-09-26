using AutoMapper;
using IntelectahClinic.DTOs.Unidade;
using IntelectahClinic.Repository;
using Microsoft.AspNetCore.Mvc;

namespace IntelectahClinic.Service;

public class UnidadeService
{
    private IntelectahClinicContext _context;
    private IMapper _mapper;

    public UnidadeService(IntelectahClinicContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper; 
    }

    public IEnumerable<DadosUnidadeDTO> BuscaUnidades()
    {
        return _mapper.Map<List<DadosUnidadeDTO>>(_context.Unidades);
    }
}
