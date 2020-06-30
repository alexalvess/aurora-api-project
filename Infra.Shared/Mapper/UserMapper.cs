using System.Collections.Generic;
using System.Linq;
using Aurora.Domain.Entities;
using Aurora.Domain.Models;

namespace Infra.Shared.Mapper
{
    public static class UserMapper
    {
        public static User ConvertToUserEntity(this CreateUserModel userModel) =>
            new User(0, userModel.Name, userModel.BirthDate, userModel.Cpf);

        public static User ConvertToUserEntity(this UpdateUserModel userModel) =>
            new User(userModel.Id, userModel.Name, userModel.BirthDate, userModel.Cpf);

        public static IEnumerable<UserModel> ConvertToUsers(this IList<User> users) =>
            new List<UserModel>(users.Select(s => new UserModel(s.Id, s.Name, s.BirthDate, s.Cpf)));

        public static UserModel ConvertToUser(this User user) =>
            new UserModel(user.Id, user.Name, user.BirthDate, user.Cpf);
    }
}
