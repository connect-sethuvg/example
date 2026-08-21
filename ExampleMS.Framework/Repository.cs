using ExampleMS.Framework.Data;
using ExampleMS.Framework.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Text;

namespace ExampleMS.Framework
{
    public class Repository<Tcontract, TEntity> : IRepository<Tcontract> where Tcontract : IEntity where TEntity : class, Tcontract
    {
        private IQueryable<TEntity>? _queryable = null;
        private bool disposedValue = false;
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbset;
        private static readonly object Lockobject = new();
        private static readonly List<Expression<Func<TEntity, object>>> navigationproperties = new();
        protected readonly ILogger _logger;
        public Repository(DbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            _dbset = dbContext.Set<TEntity>();
            @_queryable = _dbset;

        }
        private IQueryable<Tcontract> GetQuerable()
        {
            return @_queryable;
        }

        public DbContext GetDbContext()
        {
            return _dbContext;
        }

        public Tcontract Add(Tcontract entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.EditedDate = DateTime.Now;
            _dbContext.Entry((TEntity)entity).State = EntityState.Added;
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity> entityItem = _dbset.Add((TEntity)entity);
            return entityItem.Entity;
        }

        public void Delete(Tcontract entity)
        {
            if(_dbContext.Entry((TEntity)entity).State == EntityState.Detached)
            {
                _ = _dbset.Attach((TEntity)entity);
            }
            _ = _dbset.Remove((TEntity)entity);
        }

        public void DeleteAll(IEnumerable<Tcontract> entities)
        {
            lock (Lockobject)
            {
                IEnumerable<TEntity> items = entities.Cast<TEntity>();
                foreach (TEntity item in items)
                {
                    if(_dbContext.Entry(item).State== EntityState.Detached)
                    {
                        _=_dbset.Attach(item);
                    }
                    _= _dbset.Remove(item);
                }
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                
            }
            disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public async Task<IEnumerable<Tcontract>> GetAllAsync()
        {
            IEnumerable<Tcontract> items = await _dbset.ToListAsync();
            return items;
        }

        public async Task<IEnumerable<Tcontract>> GetAllAsync(Expression<Func<Tcontract, bool>> condition)
        {
            IEnumerable<Tcontract> items = await _dbset.AsNoTracking().Where(condition).ToListAsync();
            return items;
        }

        public async Task<IEntity> GetByIdAsync(int id)
        {
            return await _dbset.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public IQueryable<Tcontract> Entities
        {
            get
            {
                IQueryable<Tcontract> entities = GetQuerable().AsQueryable<Tcontract>();
                return entities;
            }
        }

        IQueryable<IEntity> IRepository<Tcontract>.Entities => throw new NotImplementedException();

        public void Include(Expression<Func<TEntity, object>> navigationProperty)
        {
            _queryable = _queryable.Include(navigationProperty);
        }

        public void Insert(IEnumerable<Tcontract> entities)
        {
            lock (Lockobject)
            {
                IEnumerable<TEntity> items = entities.Cast<TEntity>();
                foreach (TEntity item in items)
                {
                    item.CreatedDate = DateTime.Now;
                    item.EditedDate = DateTime.Now;

                }
                _dbset.AddRange(items);
            }
        }

        public void Update(Tcontract entity)
        {
            entity.EditedDate = DateTime.UtcNow;
            //_dbSet.Attach((TEntity)entity);
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity> entry = _dbContext.Entry((TEntity)entity);
            entry.State = EntityState.Modified;
            entry.Property(x => x.CreatedDate).IsModified = false;
        }

        public async Task UpdateAsync(Expression<Func<Tcontract, bool>> condition, Action<Tcontract> update)
        {
            await _dbset.Where(condition).Select(x=> x).ForEachAsync(update);
        }

        public void Update(IEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
