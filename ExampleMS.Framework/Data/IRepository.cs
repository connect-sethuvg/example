using ExampleMS.Framework.Data.Entities;
using System.Linq.Expressions;

namespace ExampleMS.Framework.Data
{
    public interface IRepository<TEntity> : IDisposable where TEntity : IEntity
    {
        IQueryable<IEntity> Entities { get; }
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> condition);
        Task<IEntity> GetByIdAsync (int id);
        TEntity Add(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);
        void Update(IEntity entity);
        Task UpdateAsync (Expression<Func<TEntity,bool>> condition, Action<TEntity> update);
        void Delete(TEntity entity);
        void DeleteAll(IEnumerable<TEntity> entities);

    }
}