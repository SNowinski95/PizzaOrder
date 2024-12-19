using DataAccessGeneral.Interfaces.Reporsitory;
using DataAccessSql.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessSql;

public static class RepositoryRegistration
{
    public static void RegisterRepository(this IServiceCollection service)
    {
        service.AddScoped<IOrderRepository, OrderRepository>();
        service.AddScoped<IPizzaRepository, PizzaRepository>();
        service.AddScoped<IIngredientRepository, IngredientRepository>();
    }
}