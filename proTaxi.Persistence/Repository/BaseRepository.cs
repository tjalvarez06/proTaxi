

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using proTaxi.Domain.Interfaces;
using System.Linq.Expressions;

namespace proTaxi.Persistence.Repository
{
    public abstract class BaseRepository<TEntity, TData, TType> : IRepository<TEntity, TData, TType> where TEntity : class
    {
        private readonly DbContext _dbContext;
        private DbSet<TEntity> _dbSet;
        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }
        public Task<TData> Exists(Expression<Func<TEntity, bool>> filter)
        {
            TData data = null;
            try
            {

            }
            catch (Exception ex)
            {
              
            }
        }

        public Task<TData> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TData> GetEntityBy(TType Id)
        {
            throw new NotImplementedException();
        }

        public Task<TData> Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TData> Save(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TData> Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
