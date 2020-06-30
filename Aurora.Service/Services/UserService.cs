using System.Collections.Generic;
using Aurora.Domain.Interfaces;
using Aurora.Domain.Models;
using Flunt.Validations;
using Infra.Shared.Contexts;
using Infra.Shared.Mapper;

namespace Aurora.Service.Services
{
    public class UserService : IServiceUser
    {
        private readonly IRepositoryUser _repositoryUser;
        private readonly NotificationContext _notificationContext;

        public UserService(IRepositoryUser repositoryUser, NotificationContext notificationContext)
        {
            _repositoryUser = repositoryUser;
            _notificationContext = notificationContext;
        }

        public IEnumerable<UserModel> RecoverAll()
        {
            var users = _repositoryUser.GetAll();
            return users.ConvertToUsers();
        }

        public UserModel RecoverById(int id)
        {
            var user = _repositoryUser.GetById(id);
            return user.ConvertToUser();
        }

        public void Delete(int id) =>
            _repositoryUser.Remove(id);

        public UserModel Insert(CreateUserModel userModel)
        {
            var user = userModel.ConvertToUserEntity();
            _notificationContext.AddNotifications(user.Notifications);

            if (_notificationContext.Invalid)
                return default;

            _repositoryUser.Save(user);
            return user.ConvertToUser();
        }


        public UserModel Update(int id, UpdateUserModel userModel)
        {
            if (id != userModel.Id)
            {
                _notificationContext.AddNotifications(
                    new Contract().AreNotEquals(id, userModel.Id, nameof(id), "User not found."));

                if (_notificationContext.Invalid)
                    return default;
            }

            var user = userModel.ConvertToUserEntity();
            _notificationContext.AddNotifications(user.Notifications);

            if (_notificationContext.Invalid)
                return default;

            _repositoryUser.Save(user);
            return user.ConvertToUser();
        }
    }
}
