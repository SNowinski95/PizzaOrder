using Dapper;
using DataAccessGeneral.Interfaces;
using DataAccessGeneral.Interfaces.Reporsitory;
using System.Data;

namespace DataAccessSql.Repository;

public abstract class DapperBaseRepository<T> : IRepository<T> where T : IId
{
    protected abstract string TableName { get; }
    protected readonly DapperContext Context;

    protected DapperBaseRepository(DapperContext context)
    {
        Context = context;
    }
    
    public virtual async Task<List<T>> GetAllAsync()
    {
        using var connection = Context.CreateConnection();
        var query = await connection.QueryAsync<T>($"SELECT * FROM {TableName}");
        return query.ToList();
    }

    public virtual async Task<T> GetByIdAsync(Guid id)
    {

        var parameters = new DynamicParameters();
        parameters.Add("Id", id.ToString(), DbType.String);
        using var connection = Context.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {TableName} WHERE Id = @Id", parameters);
    }


    public abstract Task InsertAsync(T entity);

    public abstract Task UpdateAsync(T entity);

    public virtual async Task DeleteAsync(Guid id)
    {

        var parameters = new DynamicParameters();
        parameters.Add("Id", id.ToString(), DbType.String);
        using var connection = Context.CreateConnection();
        await connection.ExecuteAsync($"DELETE FROM {TableName} WHERE Id = @Id", parameters);
    }
}