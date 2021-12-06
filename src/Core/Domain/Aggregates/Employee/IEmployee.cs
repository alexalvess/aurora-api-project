using Domain.Abstractions.Aggregates;
using System;

namespace Domain.Aggregates.Employee;

public interface IEmployee : IAggregateRoot<Guid> { }