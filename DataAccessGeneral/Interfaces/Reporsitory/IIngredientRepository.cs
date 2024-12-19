using DataAccessGeneral.Models;

namespace DataAccessGeneral.Interfaces.Reporsitory;

public interface IIngredientRepository: IRepository<Ingredient>
{
    Task InsertForPizzaAsync(Ingredient entity);
}