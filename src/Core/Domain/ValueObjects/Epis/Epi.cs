using Domain.Abstractions.ValueObjects;
using MongoDB.Bson;
using System;

namespace Domain.ValueObjects.Epis;

public record Epi : ValueObject
{
    public ObjectId Id { get; init; }

    public DateOnly Expiration { get; init; }

    protected override bool Validate()
    {
        throw new NotImplementedException();
    }
}