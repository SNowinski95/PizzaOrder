using DataAccessGeneral.Models;

namespace Services.Dto;

public class PizzaDto : IdDto
{
    public List<IngredientDto> Ingridients { get; set; }
}