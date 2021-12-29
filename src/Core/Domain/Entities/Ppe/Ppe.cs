using Domain.Abstractions.Entities;
using System;

namespace Domain.Entities.Ppe;

public class Ppe : Entity
{
    public string Name { get; init; }

    public string Description { get; init; }

    public DateOnly ManufacturingDate { get; init; }

    public long Durability { get; init; }

    public string ApprovalCertificate { get; init; }

    protected override bool Validate()
        => OnValidate<PpeValidator, Ppe>();
}
