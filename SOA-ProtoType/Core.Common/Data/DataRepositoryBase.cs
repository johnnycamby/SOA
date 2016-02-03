using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core.Common.Utils;
using Core.Contracts;

namespace Core.Common.Data
{
    public abstract class DataRepositoryBase<T, TU> : IDataRepository<T> 
        where T : class , IIdentifiableEntity, new()
        where TU : DbContext, new()
    {

        protected abstract T AddEntity(TU dbctx, T entity);
        protected abstract T UpdateEntity(TU dbctx, T entity);
        protected abstract IEnumerable<T> GetEntities(TU dbctx);
        protected abstract T GetEntity(TU dbctx, int id);

        public T Add(T entity)
        {
            using (var dbctx = new TU())
            {
                var addEntity = AddEntity(dbctx, entity);
                dbctx.SaveChanges();
                return addEntity;
            }
        }

        public void Remove(T entity)
        {
            using (var dbctx = new TU())
            {
                dbctx.Entry<T>(entity).State = EntityState.Deleted;
                dbctx.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            using (var dbctx = new TU())
            {
                var entity = GetEntity(dbctx, id);
                dbctx.Entry<T>(entity).State = EntityState.Deleted;
                dbctx.SaveChanges();
            }
        }

        public T Update(T entity)
        {
            using (var dbctx = new TU())
            {
                var existingEntity = UpdateEntity(dbctx, entity);

                SimpleMapper.PropertyMap(entity, existingEntity);
                dbctx.SaveChanges();
                return existingEntity;
            }
        }

        public IEnumerable<T> Get()
        {
            using (var dbctx = new TU())
            {
                return (GetEntities(dbctx)).ToArray().ToList();
            }
        }

        public T Get(int id)
        {
            using (var dbctx = new TU())
            {
                return GetEntity(dbctx, id);
            }
        }
    }
}