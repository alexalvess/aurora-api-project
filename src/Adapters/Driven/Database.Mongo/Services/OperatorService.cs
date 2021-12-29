using Application.Ports.MongoServices;
using DataBase.Mongo.Repositories.OperatorRepository;
using Domain.Aggregates.Employee.Operator;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DataBase.Mongo.Services;

public class OperatorService : IOperatorService
{
    private readonly IOperatorRepository _operatorRepository;

    public OperatorService(IOperatorRepository operatorRepository)
        => _operatorRepository = operatorRepository;

    public Task SaveNewOperatorAsync(Operator @operator, CancellationToken cancellationToken)
        => _operatorRepository.SaveAsync(@operator, cancellationToken);

    public Task UpdateOperatorAsync(Operator @operator, CancellationToken cancellationToken)
        => _operatorRepository.Upsert(prop => prop.Id.Equals(@operator.Id), @operator, cancellationToken);

    public Task<Operator> GetOperatorByIdAsync(ObjectId workerId, CancellationToken cancellationToken)
        => _operatorRepository.FindAsync<Operator>(prop => prop.Id.Equals(workerId), cancellationToken);

    public Task<IEnumerable<Operator>> GetAllOperators(List<string> fields, CancellationToken cancellationToken)
        => _operatorRepository.GetAllAsync<Operator>(fields, cancellationToken);


}