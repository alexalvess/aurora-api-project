using Application.Ports.MongoServices;
using DataBase.Mongo.Repositories;
using Domain.Aggregates.Employee.Operator;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DataBase.Mongo.Services;

public class OperatorService : IOperatorService
{
    private readonly IWorkerRepository _workerRepository;

    public OperatorService(IWorkerRepository workerRepository)
        => _workerRepository = workerRepository;

    public Task SaveNewOperatorAsync(Operator @operator, CancellationToken cancellationToken)
        => _workerRepository.SaveAsync(@operator, cancellationToken);

    public Task UpdateOperatorAsync(Operator @operator, CancellationToken cancellationToken)
        => _workerRepository.Upsert<Operator>(prop => prop.Id.Equals(@operator.Id), @operator, cancellationToken);

    public Task<Operator> GetOperatorByIdAsync(ObjectId workerId, CancellationToken cancellationToken)
        => _workerRepository.FindAsync<Operator>(prop => prop.Id.Equals(workerId), cancellationToken);

    public Task<List<Operator>> GetAllOperators(CancellationToken cancellationToken)
        => _workerRepository.GetAllAsync<Operator>(cancellationToken);


}