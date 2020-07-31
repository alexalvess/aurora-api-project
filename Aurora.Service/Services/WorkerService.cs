using System.Collections.Generic;
using Aurora.Domain.Interfaces;
using Aurora.Domain.Models;
using Flunt.Validations;
using Infra.Shared.Contexts;
using Infra.Shared.Mapper;

namespace Aurora.Service.Services
{
    public class WorkerService : IServiceWorker
    {
        private readonly IRepositoryWorker _repositoryUser;
        private readonly NotificationContext _notificationContext;

        public WorkerService(IRepositoryWorker repositoryUser, NotificationContext notificationContext)
        {
            _repositoryUser = repositoryUser;
            _notificationContext = notificationContext;
        }

        public IEnumerable<WorkerModel> RecoverAll()
        {
            var users = _repositoryUser.GetAll();
            return users.ConvertToUsers();
        }

        public WorkerModel RecoverById(int id)
        {
            var user = _repositoryUser.GetById(id);
            return user.ConvertToUser();
        }

        public void Delete(int id) =>
            _repositoryUser.Remove(id);

        public WorkerModel Insert(CreateWorkerModel userModel)
        {
            var user = userModel.ConvertToUserEntity();
            _notificationContext.AddNotifications(user.Notifications);

            if (_notificationContext.Invalid)
                return default;

            _repositoryUser.Save(user);
            return user.ConvertToUser();
        }


        public WorkerModel Update(int id, UpdateWorkerModel userModel)
        {
            if (id != userModel.Id)
            {
                _notificationContext.AddNotifications(new Contract().AreNotEquals(id, userModel.Id, nameof(id), "User not found."));

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
