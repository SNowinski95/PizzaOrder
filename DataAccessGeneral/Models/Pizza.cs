namespace DataAccessGeneral.Models;

public class Pizza : BaseEntity
{
    public List<Ingredient> Ingridients { get; set; } = new();

}