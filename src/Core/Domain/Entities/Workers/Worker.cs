using Domain.Abstractions.Entities;
using Domain.ValueTypes;
using MongoDB.Bson;
using System;

namespace Domain.Entities.Workers;

public class Worker : Entity<ObjectId>
{
    public Name Name { get; init; }

    public Nin Nin { get; init; }

    public Password Password { get; init; }

    public DateOnly BirthDate { get; init; }

    protected override bool Validate()
    {
        AddErrors(Name.Errors);
        AddErrors(Nin.Errors);
        AddErrors(Password.Errors);

        return OnValidate<WorkerValidator, Worker>();
    }
}

