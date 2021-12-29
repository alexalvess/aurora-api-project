using Domain.Abstractions.ValueObjects;
using MongoDB.Bson;
using System;

namespace Domain.ValueObjects.Ppes;

public record Ppe : ValueObject
{
    public ObjectId InventoryId { get; init; }

    public DateOnly Expiration { get; init; }

    protected override bool Validate()
        => OnValidate<PpeValidator, Ppe>();
}