using DataAccessGeneral.Models;

namespace Services.Dto;

public class OrderDto : IdDto
{
    public PizzaDto Pizza { get; set; }
    public string Person { get; set; }
}