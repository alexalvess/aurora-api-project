using System.Collections.Generic;
using System.Linq;
using Aurora.Domain.Entities;
using Aurora.Domain.Models;

namespace Infra.Shared.Mapper
{
    public static class WorkerMapper
    {
        public static Worker ConvertToUserEntity(this CreateWorkerModel userModel) =>
            new Worker(0, userModel.Name, userModel.BirthDate, userModel.Nin, userModel.Password);

        public static Worker ConvertToUserEntity(this UpdateWorkerModel userModel) =>
            new Worker(userModel.Id, userModel.Name, userModel.BirthDate, userModel.Nin);

        public static IEnumerable<WorkerModel> ConvertToUsers(this IList<Worker> users) =>
            new List<WorkerModel>(users.Select(s => new WorkerModel(s.Id, s.Name.ToString(), s.BirthDate, s.Nin.ToString())));

        public static WorkerModel ConvertToUser(this Worker user) =>
            new WorkerModel(user.Id, user.Name.ToString(), user.BirthDate, user.Nin.ToString());
    }
}
