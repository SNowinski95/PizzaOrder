using DataAccessGeneral.Interfaces.Reporsitory;
using DataAccessGeneral.Models;
using DataAccessNoSql;
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
        service.AddScoped<INoSqlReposytory<Pizza>, NoSqlReposytory<Pizza>>();
    }
}