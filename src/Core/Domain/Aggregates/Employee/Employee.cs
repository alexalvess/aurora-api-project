using Domain.Abstractions.Aggregates;
using Domain.ValueTypes;
using System;
using System.Collections.Generic;

namespace Domain.Aggregates.Employee;

public abstract class Employee : AggregateRoot<Guid>, IEmployee
{
    public bool Active { get; protected set; } = true;

    public Name Name { get; init; }

    public Nin Nin { get; init; }

    public Password Password { get; init; }

    public DateOnly BirthDate { get; init; }

    public DateOnly AdmissionDate { get; private set; } = DateOnly.FromDateTime(DateTime.Now);

    public DateOnly? TerminationDate { get; protected set; }

    //public abstract ICollection<TReturn> TurnOffEmployee<TReturn>() where TReturn : class;

    protected override bool Validate()
    {
        AddErrors(Name.Errors);
        AddErrors(Nin.Errors);
        AddErrors(Password.Errors);

        return OnValidate<EmployeeValidator, Employee>();
    }
}