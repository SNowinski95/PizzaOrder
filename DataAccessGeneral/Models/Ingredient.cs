namespace DataAccessGeneral.Models;

public class Ingredient : BaseEntity
{
    
    public string Name { get; set; }

    public string? Description { get; set; }

    public Guid? PizzaId { get; set; }
}
