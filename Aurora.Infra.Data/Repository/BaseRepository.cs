using System.Collections.Generic;
using System.Linq;
using Aurora.Domain.Entities;
using Aurora.Infra.Data.Context;

namespace Aurora.Infra.Data.Repository
{
    public class BaseRepository<T, B> where T : BaseEntity<B>
    {
        protected readonly MySqlContext _mySqlContext;

        public BaseRepository(MySqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }

        protected virtual void Insert(T obj)
        {
            _mySqlContext.Set<T>().Add(obj);
            _mySqlContext.SaveChanges();
        }

        protected virtual void Update(T obj)
        {
            _mySqlContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _mySqlContext.SaveChanges();
        }

        protected virtual void Delete(int id)
        {
            _mySqlContext.Set<T>().Remove(Select(id));
            _mySqlContext.SaveChanges();
        }

        protected virtual IList<T> Select() =>
            _mySqlContext.Set<T>().ToList();

        protected virtual T Select(int id) =>
            _mySqlContext.Set<T>().Find(id);
    }
}
