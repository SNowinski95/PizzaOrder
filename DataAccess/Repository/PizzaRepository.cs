using Dapper;
using DataAccessGeneral.Interfaces.Reporsitory;
using DataAccessGeneral.Models;
using System.Data;
using static Dapper.SqlMapper;

namespace DataAccessSql.Repository;

public class PizzaRepository : DapperBaseRepository<Pizza>, IPizzaRepository
{
    private readonly IIngredientRepository _ingredientRepository;
    protected override string TableName => "Pizzas";

    public PizzaRepository(DapperContext context, IIngredientRepository ingredientRepository) : base(context)
    {
        _ingredientRepository = ingredientRepository;
    }

    public override async Task<Pizza?> GetByIdAsync(Guid id)
    {
        var sqlQuery = @$"SELECT * FROM  {TableName} 
                    JOIN {Consts.TableNameConst.Ingredients} ON {TableName}.{nameof(Pizza.Id)} = {Consts.TableNameConst.Ingredients}.{nameof(Ingredient.PizzaId)} 
                    WHERE {TableName}.Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("Id", id.ToString(), DbType.String);
        using var connection = Context.CreateConnection();
        var res = (await connection.QueryAsync< Pizza, Ingredient, Pizza>(sqlQuery, map: (p, i) =>
        {
            p.Ingridients.Add(i);
            return p;
        }, param: parameters)).ToList();
        if (!res.ToList().Any()) return null;
        return res.GroupBy(n => n.Id).Select(n =>
        {
            var first = n.First();
            first.Ingridients = n.SelectMany(m => m.Ingridients).ToList();
            return first;
        }).SingleOrDefault();
        //connection.QuerySingleOrDefault<Order>(sqlQuery,id,)
    }

    public override async Task InsertAsync(Pizza entity)
    {
        if(entity.Id == Guid.Empty) entity.Id = Guid.NewGuid();
        var query = $"INSERT INTO {TableName} (Id) VALUES (@Id);";
        var parameters = new DynamicParameters();
        parameters.Add("Id", entity.Id.ToString(), DbType.String);
        using var connection = Context.CreateConnection();
        await connection.ExecuteAsync(query,parameters);
        foreach (var item in entity.Ingridients)
        {
            item.PizzaId = entity.Id;
            await _ingredientRepository.InsertForPizzaAsync(item);
        }
    }

    public override async Task UpdateAsync(Pizza entity)
    {
        throw new NotImplementedException("no needed");
    }

    public override async Task DeleteAsync(Guid id)
    {
        var obj = await GetByIdAsync(id);
        foreach (var item in obj.Ingridients)
        {
            await _ingredientRepository.DeleteAsync(item.Id);
        }

        await base.DeleteAsync(id);
    }
}