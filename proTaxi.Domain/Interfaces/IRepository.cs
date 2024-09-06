

using System.Formats.Tar;
using System.Linq.Expressions;

namespace proTaxi.Domain.Interfaces
{
    public interface IRepository<TEntity, TData, TType> where TEntity : class
    {
        /// <summary>
        /// Save Entity
        /// </summary>
        /// <param name="entity"></param>
        Task<TData> Save(TEntity entity);
        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity"></param>
        Task<TData> Update(TEntity entity);
        /// <summary>
        /// Remove Entity
        /// </summary>
        /// <param name="entity"></param>
        Task<TData> Remove(TEntity entity);
        /// <summary>
        /// Get All data
        /// </summary>
        /// <returns></returns>>
        Task<TData> GetAll();
        /// <summary>
        /// Get Entity by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<TData> GetEntityBy(TType Id);
        /// <summary>
        /// Exists entity
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<TData> Exists(Expression<Func<TEntity, bool>> filter);
    }
}
