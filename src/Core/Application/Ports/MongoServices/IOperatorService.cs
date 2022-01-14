using Application.Abstractions.Pagination;
using Domain.Aggregates.Employee.Operator;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Ports.MongoServices;

public interface IOperatorService
{
    Task SaveNewOperatorAsync(Operator @operator, CancellationToken cancellationToken);

    Task UpdateOperatorAsync(Operator @operator, CancellationToken cancellationToken);

    Task<Operator> GetOperatorByIdAsync(ObjectId operatorId, CancellationToken cancellationToken);

    Task<IPagedResult<Operator>> GetAllOperators(Paging paging, CancellationToken cancellationToken);
}
