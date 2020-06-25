using System.Collections.Generic;
using Aurora.Domain.Entities;

namespace Aurora.Domain.Interfaces
{
    public interface IRepositoryUser
    {
        void Save(User obj);

        void Remove(int id);

        User GetById(int id);

        IList<User> GetAll();
    }
}

