using Application.DataTransferObject;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Ports.DomainServices;

public interface IWorkerDomainService
{
    Task RegisterWorkerAsync(RegisterWorkerDto registerWorkerDto, CancellationToken cancellationToken);
}
