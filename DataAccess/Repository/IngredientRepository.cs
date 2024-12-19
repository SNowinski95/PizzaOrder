using Dapper;
using DataAccessGeneral.Interfaces.Reporsitory;
using DataAccessGeneral.Models;
using System.Data;

namespace DataAccessSql.Repository;

public class IngredientRepository : DapperBaseRepository<Ingredient>, IIngredientRepository
{
    public IngredientRepository(DapperContext context) : base(context)
    {
    }
    protected override string TableName => "Ingredients";
    public override async Task InsertAsync(Ingredient entity)
    {
        if (entity.Id == Guid.Empty) entity.Id = Guid.NewGuid();
        var query = $"INSERT INTO {TableName} (Id,Name,Description) VALUES (@Id,@Name,@Description)";
        var parameters = new DynamicParameters();
        parameters.Add("Id", entity.Id.ToString(), DbType.String);
        parameters.Add("Name", entity.Name, DbType.String);
        parameters.Add("Description", entity.Description, DbType.String);
        using var connection = Context.CreateConnection();
        await connection.ExecuteAsync(query, parameters);
    }

    public override async Task UpdateAsync(Ingredient entity)
    {
        var query = $"UPDATE {TableName} SET Name = @Name, Description = @Description WHERE Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("Id", entity.Id.ToString(), DbType.String);
        parameters.Add("Name", entity.Name, DbType.String);
        parameters.Add("Description", entity.Description, DbType.String);
        using var connection = Context.CreateConnection();
        await connection.ExecuteAsync(query, parameters);
    }
    public async Task InsertForPizzaAsync(Ingredient entity)
    {
        if (entity.Id == Guid.Empty) entity.Id = Guid.NewGuid();
        var query = $"INSERT INTO {TableName} (Id,Name,PizzaId) VALUES (@Id,@Name,@PizzaId)";
        var parameters = new DynamicParameters();
        parameters.Add("Id", entity.Id.ToString(), DbType.String);
        parameters.Add("Name", entity.Name, DbType.String);
        parameters.Add("PizzaId", entity.PizzaId.ToString(), DbType.String);
        using var connection = Context.CreateConnection();
        await connection.ExecuteAsync(query, parameters);
    }
}