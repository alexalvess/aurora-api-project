using Domain.Enumerations;
using Domain.ValueObjects.Epis;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Aggregates.Employee.Operator;

public class Operator : Employee
{
    public WorkShift WorkShift { get; private set; } = WorkShift.MorningShift;

    public ICollection<Epi> Epis { get; private set; }

    public void ChangeShift(WorkShift workShift)
        => WorkShift = workShift;

    public void AddApis(List<Epi> epis)
        => epis.ForEach(epi => Epis.Add(epi));

    public IReadOnlyCollection<Epi> RemoveExpiredEpis()
    {
        var experied = Epis.Where(epi => epi.Expiration < DateOnly.FromDateTime(DateTime.Now));

        Epis = Epis.Where(epi => epi.Expiration >= DateOnly.FromDateTime(DateTime.Now)).ToList();

        return experied.ToList();
    }

    public Epi ReturnEpi(ObjectId epiId)
    {
        var epi = Epis.First(epi => epi.Id.Equals(epiId));

        Epis.Remove(epi);

        return epi;
    }
}