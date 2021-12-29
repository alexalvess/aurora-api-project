using Application.Ports.MongoServices;
using DataBase.Mongo.Repositories.InventoryRepository;
using Domain.Aggregates.Inventory;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DataBase.Mongo.Services;

public class InventoryService : IInventoryService
{
    private readonly IInventoryRepository _inventoryRepository;

    public InventoryService(IInventoryRepository inventoryRepository)
        => _inventoryRepository = inventoryRepository;

    public Task SaveNewInventoryAsync(Inventory inventory, CancellationToken cancellationToken)
        => _inventoryRepository.SaveAsync(inventory, cancellationToken);

    public Task<Inventory> GetInventoryByIdAsync(ObjectId inventoryId, CancellationToken cancellationToken)
        => _inventoryRepository.FindAsync<Inventory>(inventory => inventory.Id.Equals(inventoryId), cancellationToken);

    public Task<List<Inventory>> GetAllInventoryAsync(CancellationToken cancellationToken)
        => default;// _inventoryRepository.GetAllAsync<Inventory>(cancellationToken);

    public Task UpdateInventoryAsync(Inventory inventory, CancellationToken cancellationToken)
        => _inventoryRepository.Upsert(item => item.Id.Equals(inventory.Id), inventory, cancellationToken);
}