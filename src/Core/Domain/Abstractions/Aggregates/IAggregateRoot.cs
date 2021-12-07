namespace Domain.Abstractions.Aggregates;

public interface IAggregateRoot<out TId> 
    where TId : struct
{
    TId Id { get; }
}