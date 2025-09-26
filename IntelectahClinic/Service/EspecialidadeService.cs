using AutoMapper;
using IntelectahClinic.DTOs.Especialidade;
using IntelectahClinic.Models;
using IntelectahClinic.Models.enums;
using IntelectahClinic.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntelectahClinic.Service;

public class EspecialidadeService
{
    private IntelectahClinicContext _context;
    private IMapper _mapper;

    public EspecialidadeService(IntelectahClinicContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<TipoServicoDTO> GetTipoServico()
    {
        return Enum.GetValues(typeof(TipoServico))
                   .Cast<TipoServico>()
                   .Select(t => new TipoServicoDTO { Id = (int)t, Nome = t.ToString() })
                   .ToList();
    }

    public List<DadosEspecialidadeDTO> GetEspecialidades([FromQuery] TipoServico? tipo)
    {
        List<Especialidade> listaEspecialidades = _context.Especialidades.Where(e => e.Tipo == tipo.Value).ToList();

        return _mapper.Map<List<DadosEspecialidadeDTO>>(listaEspecialidades);

    }
}
