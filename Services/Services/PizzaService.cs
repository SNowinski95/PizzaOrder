using AutoMapper;
using DataAccessGeneral.Interfaces.Reporsitory;
using DataAccessGeneral.Models;
using Services.Dto;
using Services.Interfaces;

namespace Services.Services;

public class PizzaService : ServiceBase<PizzaDto,Pizza>, IPizzaService
{
    public PizzaService(IPizzaRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}