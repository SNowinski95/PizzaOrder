using System.Transactions;
using AutoMapper;
using DataAccessGeneral.Interfaces;
using DataAccessGeneral.Interfaces.Reporsitory;
using Services.Interfaces;

namespace Services.Services
{
    public abstract class ServiceBase<T, TEntity> : IServiceBase<T> where T : IIdDto where TEntity : IId
    {
        protected IRepository<TEntity> Repository;
        private readonly IMapper _mapper;

        protected ServiceBase(IRepository<TEntity> repository, IMapper mapper)
        {
            Repository = repository;
            _mapper = mapper;
        }

        public async Task<List<T>> GetAllAsync()
        {
            var entityList = await Repository.GetAllAsync();
            return _mapper.Map<List<T>>(entityList);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await Repository.GetByIdAsync(id);
                return _mapper.Map<T>(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public async Task InsertAsync(T dto)
        {
            try
            {
                using var transaction = new TransactionScope();
                await Repository.InsertAsync(_mapper.Map<TEntity>(dto));
                transaction.Complete();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task UpdateAsync(T dto)
        {
            using var transaction = new TransactionScope();
            await Repository.UpdateAsync(_mapper.Map<TEntity>(dto));
            transaction.Complete();
        }

        public async Task DeleteAsync(Guid id)
        {
            await Repository.DeleteAsync(id);
        }
    }
}