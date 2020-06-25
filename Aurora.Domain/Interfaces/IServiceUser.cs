using System.Collections.Generic;
using Aurora.Domain.Entities;

namespace Aurora.Domain.Interfaces
{
    public interface IServiceUser
    {
        User Insert(User obj);

        User Update(User obj);

        void Delete(int id);

        User RecoverById(int id);

        IList<User> Browse();
    }
}
