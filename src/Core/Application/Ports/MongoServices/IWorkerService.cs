using Domain.Entities.Workers;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Ports.MongoServices;
public interface IWorkerService
{
    Task SaveNewWorkerAsync(Worker worker, CancellationToken cancellationToken);

    Task UpdateWorkerAsync(Worker worker, CancellationToken cancellationToken);

    Task<Worker> GetWorkerByIdAsync(ObjectId workerId, CancellationToken cancellationToken);

    Task<List<Worker>> GetAllWorkers(CancellationToken cancellationToken);
}
