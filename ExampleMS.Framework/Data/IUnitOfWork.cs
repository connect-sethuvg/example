using ExampleMS.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Principal;
using System.Text;

namespace ExampleMS.Framework.Data
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        /// 
        IEnumerable<TEntity> Exec<TEntity>(string query, params object[] parameters);

        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity;

        int Commit();
        void BeginTransaction();
        Task<int> CommitAsync();
        void Rollback();
        void Dispose(bool disposing);

    }
}
