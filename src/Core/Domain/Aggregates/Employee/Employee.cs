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

    [BsonSerializer(typeof(StructBsonSerializer<Name>))]
    public Name Name { get; init; }

    [BsonSerializer(typeof(StructBsonSerializer<Nin>))]
    public Nin Nin { get; init; }

    [BsonSerializer(typeof(StructBsonSerializer<Password>))]
    public Password Password { get; init; }

    public DateTime BirthDate { get; init; }

    public DateTime AdmissionDate { get; private set; } = DateTime.Now;

    public DateTime? TerminationDate { get; protected set; }

    public virtual void TurnOffEmployee()
    {
        Active = false;
        TerminationDate = DateTime.Now;
    }

    protected override bool Validate()
    {
        AddErrors(Name.Errors);
        AddErrors(Nin.Errors);
        AddErrors(Password.Errors);

        return OnValidate<EmployeeValidator, Employee>();
    }
}