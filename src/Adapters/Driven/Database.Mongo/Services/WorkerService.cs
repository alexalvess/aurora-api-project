using Application.Ports.MongoServices;
using DataBase.Mongo.Repositories;
using Domain.Entities.Workers;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DataBase.Mongo.Services;

public class WorkerService : IWorkerService
{
    private readonly IWorkerRepository _workerRepository;

    public WorkerService(IWorkerRepository workerRepository)
        => _workerRepository = workerRepository;

    public Task SaveNewWorkerAsync(Worker worker, CancellationToken cancellationToken)
        => _workerRepository.SaveAsync(worker, cancellationToken);

    public Task UpdateWorkerAsync(Worker worker, CancellationToken cancellationToken)
        => _workerRepository.Upsert<Worker>(prop => prop.Id.Equals(worker.Id), worker, cancellationToken);

    public Task<Worker> GetWorkerByIdAsync(ObjectId workerId, CancellationToken cancellationToken)
        => _workerRepository.FindAsync<Worker>(prop => prop.Id.Equals(workerId), cancellationToken);

    public Task<List<Worker>> GetAllWorkers(CancellationToken cancellationToken)
        => _workerRepository.GetAllAsync<Worker>(cancellationToken);


}