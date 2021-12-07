using Domain.Abstractions.Entities;

namespace Domain.Abstractions.Aggregates;

public abstract class AggregateRoot<TId> : Entity, IAggregateRoot<TId>
    where TId : struct 
{
    public TId Id { get; protected set; }
}