using ExampleMS.Framework.Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleMS.Framework.Mappers
{
    /// <summary>
    /// API DataMapper
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    public abstract class APIDataMapper<TEntity, TObject> where TEntity : IEntity where TObject : class, new()
    {
        protected IServiceProvider Services { get; set; }
        protected APIDataMapper(IServiceProvider services)
        {
            this.Services = services;
        }

        public abstract TObject ToObject(TEntity entity);
        public abstract TEntity ToEntity (TObject vallue);

        protected TEntity? CreateEntity()
        {
            TEntity? entity = (TEntity?)Services.GetRequiredService(typeof(TEntity));
            return entity;
        }
        public IEnumerable<TObject> ToObjects(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                yield return ToObject(entity);
            }
        }
        public IEnumerable<TEntity> ToEntities(IEnumerable<TObject> items)
        {
            foreach (TObject item in items)
            {
                yield return ToEntity(item);
            }
        }

    }
}
