using System.Collections.Generic;
using System.Linq;
using Aurora.Domain.Entities;
using Aurora.Infra.Data.Context;

namespace Aurora.Infra.Data.Repository
{
    public class BaseRepository<T, B> where T : BaseEntity<B>
    {
        protected readonly MySqlContext _context;

        public BaseRepository()
        {
            _context = new MySqlContext();
        }

        protected virtual void Insert(T obj)
        {
            _context.Set<T>().Add(obj);
            _context.SaveChanges();
        }

        protected virtual void Update(T obj)
        {
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        protected virtual void Delete(int id)
        {
            _context.Set<T>().Remove(Select(id));
            _context.SaveChanges();
        }

        protected virtual IList<T> Select() =>
            _context.Set<T>().ToList();

        protected virtual T Select(int id) =>
            _context.Set<T>().Find(id);
    }
}
