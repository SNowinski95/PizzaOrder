using AutoMapper;
using DataAccessGeneral.Interfaces.Reporsitory;
using DataAccessGeneral.Models;
using Services.Dto;
using Services.Interfaces;

namespace Services.Services;

public class IngredientService : ServiceBase<IngredientDto, Ingredient>, IIngredientService
{
    public IngredientService(IIngredientRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}