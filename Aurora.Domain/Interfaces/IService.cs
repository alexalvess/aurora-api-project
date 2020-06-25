using System.Collections.Generic;
using Aurora.Domain.Entities;
using FluentValidation;

namespace Aurora.Domain.Interfaces
{
    public interface IService<T> where T : BaseEntity
    {
        T Insert<V>(T obj) where V : AbstractValidator<T>;

        T Update<V>(T obj) where V : AbstractValidator<T>;

        void Delete(int id);

        T Recover(int id);

        IList<T> Browser();
    }
}
