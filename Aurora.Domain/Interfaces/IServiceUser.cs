using System.Collections.Generic;
using Aurora.Domain.Models;

namespace Aurora.Domain.Interfaces
{
    public interface IServiceUser
    {
        UserModel Insert(CreateUserModel userModel);

        UserModel Update(int id, UpdateUserModel userModel);

        void Delete(int id);

        UserModel RecoverById(int id);

        IEnumerable<UserModel> RecoverAll();
    }
}
