using Application.DataTransferObject;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Ports.DomainServices;

public interface IOperatorDomainService
{
    Task<ObjectId> RegisterOperatorAsync(RegisterOperatorDto registerWorkerDto, CancellationToken cancellationToken);
}
