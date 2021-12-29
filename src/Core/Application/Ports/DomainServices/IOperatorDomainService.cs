using Application.DataTransferObject;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Ports.DomainServices;

public interface IOperatorDomainService
{
    Task<ObjectId> RegisterOperatorAsync(RegisterOperatorDto registerWorkerDto, CancellationToken cancellationToken);

    Task<RetrieveOperatorDetailsDto> RetrieveOperatorDetailsAsync(string operatorId, CancellationToken cancellationToken);

    Task<IEnumerable<RetrieveOperators>> RetrieveOperatorsAsync(CancellationToken cancellationToken);
}
