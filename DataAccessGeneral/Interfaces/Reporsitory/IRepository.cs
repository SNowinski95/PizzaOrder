namespace DataAccessGeneral.Interfaces.Reporsitory;

public interface IRepository<T> where T : IId
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task InsertAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}