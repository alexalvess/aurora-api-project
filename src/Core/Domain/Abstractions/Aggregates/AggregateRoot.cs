using Domain.Abstractions.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Abstractions.Aggregates;

public abstract class AggregateRoot<TId> : Entity, IAggregateRoot<TId>
    where TId : struct 
{
    [BsonId]
    public TId Id { get; protected set; }
}