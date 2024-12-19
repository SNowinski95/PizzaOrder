using DataAccessGeneral.Interfaces;

namespace Services.Interfaces;

public interface IServiceBase<T> where T : IIdDto
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task InsertAsync(T dto);
    Task UpdateAsync(T dto);
    Task DeleteAsync(Guid id);
}