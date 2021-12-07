using Application.DataTransferObject;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Ports.DomainServices;

public interface IOperatorDomainService
{
    Task RegisterOperatorAsync(RegisterOperatorDto registerWorkerDto, CancellationToken cancellationToken);
}
