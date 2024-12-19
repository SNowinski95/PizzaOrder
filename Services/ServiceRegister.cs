using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using Services.Services;

namespace Services;

public static class ServiceRegister
{
    public static void RegisterServices(this IServiceCollection service)
    {
        service.AddScoped<IOrderService, OrderService>();
        service.AddScoped<IPizzaService, PizzaService>();
        service.AddScoped<IIngredientService, IngredientService>();
    }
}