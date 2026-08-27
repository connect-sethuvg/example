using ExampleMS.Framework.Data;
using ExampleMS.Framework.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace ExampleMS.Framework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private IDbContextTransaction? _transaction = null;

        private IServiceProvider serviceProvider {  get; set; }
        private static readonly object _lock = new();
        private readonly ILogger _logger;

        private bool disposedValue = false; 

        public UnitOfWork( DbContext dbContext, IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;
            this.serviceProvider = serviceProvider;
            _logger = loggerFactory.CreateLogger("logs");

        }

        public void BeginTransaction()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }
        public int CommitTransaction()
        {
            lock (_lock)
            {
                try
                {
                    int result = _dbContext.SaveChangesAsync().Result;
                    _transaction.Commit();
                    return result;
                }
                finally
                {
                    //_dbContext.ChangeTracker.Entries()
                    //    .ToList()
                    //    .ForEach(x => x.State = EntityState.Detached);
                }
            }
        }

        public int Commit()
        {
            lock (_lock)
            {
                try
                {
                    int result = _dbContext.SaveChanges();
                    _transaction.Commit();
                    return result;
                }
                catch
                {
                    _transaction.Rollback();
                    return 0;
                }
                finally
                {

                }
            }
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                int result = await _dbContext.SaveChangesAsync();
                return result;
            }
            finally { }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<TEntity> Exec<TEntity>(string query, params object[] parameters)
        {
            FormattableString sql = FormattableStringFactory.Create(query, parameters);
            List<TEntity> entities = _dbContext.Database.SqlQuery<TEntity>(sql).ToList();
            return entities.Select(i => i).AsEnumerable();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity
        {
            object? instance = serviceProvider.GetService(typeof(TEntity));
            Type instanceType = instance.GetType();
            MethodInfo setMethod = GetType().GetTypeInfo().GetMethod("CreateRepository").MakeGenericMethod(typeof(TEntity), instanceType);
            IRepository<TEntity>? repository = (IRepository<TEntity>)setMethod.Invoke(this, new object[] { });
            //Repositories[keyType] = repository;
            return repository;
        }

        public virtual object CreateRepository<TContract, TEntity>() where TContract : IEntity where TEntity : class, TContract
        {
            Repository<TContract, TEntity> repository = new(_dbContext, _logger);
            return repository;
        }
    }
}
