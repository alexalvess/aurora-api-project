using Domain.Abstractions.Aggregates;
using Domain.Serializers;
using Domain.ValueTypes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Domain.Aggregates.Employee;

public abstract class Employee : AggregateRoot<ObjectId>, IEmployee
{
    public bool Active { get; protected set; } = true;

    [BsonSerializer(typeof(StructBsonSerializer))]
    public Name Name { get; init; }

    public Nin Nin { get; init; }

    public Password Password { get; init; }

    public DateOnly BirthDate { get; init; }

    public DateOnly AdmissionDate { get; private set; } = DateOnly.FromDateTime(DateTime.Now);

    public DateOnly? TerminationDate { get; protected set; }

    public virtual void TurnOffEmployee()
    {
        Active = false;
        TerminationDate = DateOnly.FromDateTime(DateTime.Now);
    }

    protected override bool Validate()
    {
        AddErrors(Name.Errors);
        AddErrors(Nin.Errors);
        AddErrors(Password.Errors);

        return OnValidate<EmployeeValidator, Employee>();
    }
}