using AutoMapper;
using DataAccessGeneral.Models;
using Services.Dto;

namespace Services.Mapping;

public class ServiceProfile :Profile
{
    public ServiceProfile()
    {
        CreateMap<Pizza, PizzaDto>().ReverseMap();
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<Ingredient, IngredientDto>().ReverseMap();
    }
}