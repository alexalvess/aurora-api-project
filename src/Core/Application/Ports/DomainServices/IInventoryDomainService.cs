using Application.DataTransferObject;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Ports.DomainServices;

public interface IInventoryDomainService
{
    Task CreateNewInventoryAsync(CreateInventoryDto createInventoryDto, CancellationToken cancellationToken);

    Task AddMorePpeAsync(AddPpeDto addPpeDto, CancellationToken cancellationToken);


}