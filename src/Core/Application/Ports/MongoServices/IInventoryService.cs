using Domain.Aggregates.Inventory;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Ports.MongoServices;

public interface IInventoryService
{
    Task SaveNewInventoryAsync(Inventory inventory, CancellationToken cancellationToken);

    Task<Inventory> GetInventoryByIdAsync(ObjectId inventoryId, CancellationToken cancellationToken);

    Task<List<Inventory>> GetAllInventoryAsync(CancellationToken cancellationToken);

    Task UpdateInventoryAsync(Inventory inventory, CancellationToken cancellationToken);
}