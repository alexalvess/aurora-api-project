using Domain.Abstractions.Entities;

namespace Domain.Abstractions.Aggregates;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>
    where TId : struct { }