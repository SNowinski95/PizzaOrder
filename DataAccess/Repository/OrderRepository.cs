using Dapper;
using DataAccessGeneral.Interfaces.Reporsitory;
using DataAccessGeneral.Models;
using System.Data;
using static Dapper.SqlMapper;

namespace DataAccessSql.Repository;

public class OrderRepository: DapperBaseRepository<Order>, IOrderRepository
{
    private readonly IPizzaRepository _pizzaRepository;
    protected override string TableName => "Orders";
    public OrderRepository(DapperContext context, IPizzaRepository pizzaRepository) : base(context)
    {
        _pizzaRepository = pizzaRepository;
    }
    public override async Task InsertAsync(Order entity)
    {
        await _pizzaRepository.InsertAsync(entity.Pizza);
        if (entity.Id == Guid.Empty) entity.Id = Guid.NewGuid();
        var query = $"INSERT INTO {TableName} (Id, PizzaId, Person) VALUES (@Id, @PizzaId, @Person)";
        var parameters = new DynamicParameters();
        parameters.Add("Id", entity.Id.ToString(), DbType.String);
        parameters.Add("PizzaId", entity.Pizza.Id.ToString(), DbType.String);
        parameters.Add("Person", entity.Person, DbType.String);
        using var connection = Context.CreateConnection();
        await connection.ExecuteAsync(query, parameters);
    }
    //is it need?
    public override async Task UpdateAsync(Order entity)
    {
        //var query = $"INSERT INTO {TableName} (Id, PizzaId, Person) VALUES (@Id, @PizzaId, @Person)";
        //var parameters = new DynamicParameters();
        //parameters.Add("Id", entity.Id, DbType.String);
        //parameters.Add("PizzaId", entity.Pizza.Id, DbType.String);
        //parameters.Add("Person", entity.Person, DbType.String);
        //using var connection = Context.CreateConnection();
        //await connection.ExecuteAsync(query, parameters);
    }

    public override async Task<Order?> GetByIdAsync(Guid id)
    {
        var sqlQuery =@$"SELECT * FROM  {TableName} 
                    JOIN {Consts.TableNameConst.Pizzas} ON {TableName}.{nameof(Order.PizzaId)} = {Consts.TableNameConst.Pizzas}.{nameof(Pizza.Id)} 
                    JOIN {Consts.TableNameConst.Ingredients} ON {TableName}.{nameof(Order.PizzaId)} = {Consts.TableNameConst.Ingredients}.{nameof(Ingredient.PizzaId)} 
                    WHERE {TableName}.Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("Id", id.ToString(), DbType.String);
        using var connection = Context.CreateConnection();
        var res = (await connection.QueryAsync<Order, Pizza, Ingredient, Order>(sqlQuery, map: (o, p, i) =>
        {
            o.Pizza = p;
            o.Pizza.Ingridients.Add(i);
            return o;
        }, param: parameters)).ToList();
        if (!res.ToList().Any()) return null;
        return res.GroupBy(n => n.Id).Select(n =>
        {
            var first = n.First();
            first.Pizza.Ingridients = n.SelectMany(m => m.Pizza.Ingridients).ToList();
            return first;
        }).SingleOrDefault();
        //connection.QuerySingleOrDefault<Order>(sqlQuery,id,)
    }

    public override async Task DeleteAsync(Guid id)
    {
        var obj= await GetByIdAsync(id);
        await _pizzaRepository.DeleteAsync(obj.PizzaId);
        await base.DeleteAsync(id);
    }
}