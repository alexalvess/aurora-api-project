using Application.DataTransferObject;
using Application.Ports.DomainServices;
using Application.Ports.MongoServices;
using Domain.Aggregates.Inventory;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DomainServices;

public class InventoryDomainService : IInventoryDomainService
{
    private readonly IInventoryService _inventoryService;

    public InventoryDomainService(IInventoryService inventoryService)
        => _inventoryService = inventoryService;

    public async Task AddMorePpeAsync(AddPpeDto addPpeDto, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryService.GetInventoryByIdAsync(addPpeDto.InventoryId, cancellationToken);
        inventory.AddAvailableQuantity(addPpeDto.Quantity);

        await _inventoryService.UpdateInventoryAsync(inventory, cancellationToken);
    }

    public async Task CreateNewInventoryAsync(CreateInventoryDto createInventoryDto, CancellationToken cancellationToken)
    {
        Inventory inventory = new()
        {
            Ppe = new()
            {
                Name = createInventoryDto.CreatePpe.Name,
                Description = createInventoryDto.CreatePpe.Description,
                Durability = createInventoryDto.CreatePpe.Durability,
                ManufacturingDate = createInventoryDto.CreatePpe.ManufacturingDate,
                ApprovalCertificate = createInventoryDto.CreatePpe.ApprovalCertificate
            }
        };

        inventory.AddAvailableQuantity(createInventoryDto.Quantity);

        await _inventoryService.SaveNewInventoryAsync(inventory, cancellationToken);
    }
}