using System.Collections.Generic;
using System.Linq;
using Aurora.Domain.Entities;
using Aurora.Infra.Data.Context;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Aurora.Infra.Data.Repository
{
    public class BaseRepository<TEntity, TKeyType> where TEntity : BaseEntity<TKeyType>
    {
        protected readonly MySqlContext _mySqlContext;

        public BaseRepository(MySqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }

        protected virtual void Insert(TEntity obj)
        {
            _mySqlContext.Set<TEntity>().Add(obj);
            _mySqlContext.SaveChanges();
        }

        protected virtual void Update(TEntity obj)
        {
            _mySqlContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _mySqlContext.SaveChanges();
        }

        protected virtual void Update<TProperty>(TEntity obj, params PropertyEntry<TEntity, TProperty>[] propsToIgnore)
        {
            _mySqlContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            foreach (var item in propsToIgnore)
                item.IsModified = false;

            _mySqlContext.SaveChanges();
        }

        protected virtual void Delete(int id)
        {
            _mySqlContext.Set<TEntity>().Remove(Select(id));
            _mySqlContext.SaveChanges();
        }

        protected virtual IList<TEntity> Select() =>
            _mySqlContext.Set<TEntity>().ToList();

        protected virtual TEntity Select(int id) =>
            _mySqlContext.Set<TEntity>().Find(id);
    }
}
