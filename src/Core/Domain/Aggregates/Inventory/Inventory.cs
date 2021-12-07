using Domain.Abstractions.Aggregates;
using Domain.Entities.Ppe;
using System;

namespace Domain.Aggregates.Inventory;

public class Inventory : AggregateRoot<Guid>
{
    public Ppe Ppe { get; init; }

    public long AvailableQuantity { get; private set; }

    public long DepreciatedQuantity { get; private set; }

    public long TotalAmount { get; private set; }

    public void AddAvailableQuantity(long quantity = 1)
    {
        AvailableQuantity += quantity;
        UpdateTotalAmount();
    }

    public void AddDepreciatedQuantity(long quantity = 1)
    {
        AvailableQuantity -= quantity;
        DepreciatedQuantity += quantity;
        UpdateTotalAmount();
    }

    private void UpdateTotalAmount()
        => TotalAmount = AvailableQuantity + DepreciatedQuantity;

    protected override bool Validate()
        => OnValidate<InventoryValidator, Inventory>();
}