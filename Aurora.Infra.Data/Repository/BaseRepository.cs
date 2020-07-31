using System.Collections.Generic;
using System.Linq;
using Aurora.Domain.Entities;
using Aurora.Infra.Data.Context;

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
