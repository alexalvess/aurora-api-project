using Domain.Abstractions.DomainEntities.Employees;
using Domain.ValueObjects.Epis;
using System.Collections.Generic;

namespace Domain.Entities.Workers;

public class Worker : Employee
{
    public ICollection<Epi> Epis { get; private set; }

    protected override bool Validate()
        => base.Validate();
}