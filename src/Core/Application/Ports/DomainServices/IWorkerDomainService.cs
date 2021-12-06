using Application.DataTransferObject;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Ports.DomainServices;

public interface IWorkerDomainService
{
    Task RegisterWorkerAsync(RegisterOperatorDto registerWorkerDto, CancellationToken cancellationToken);
}
