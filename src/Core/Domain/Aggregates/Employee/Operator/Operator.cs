using Domain.Enumerations;
using Domain.ValueObjects.Ppes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Aggregates.Employee.Operator;

public class Operator : Employee
{
    public WorkShift WorkShift { get; private set; }

    public ICollection<Ppe> Ppes { get; private set; }

    public void ChangeShift(WorkShift workShift)
        => WorkShift = workShift;

    public void AddPpes(List<Ppe> epis)
        => epis.ForEach(epi => Ppes.Add(epi));

    public IReadOnlyCollection<Ppe> RemoveExpiredPpes()
    {
        var experied = Ppes.Where(epi => epi.Expiration < DateOnly.FromDateTime(DateTime.Now));

        Ppes = Ppes.Where(epi => epi.Expiration >= DateOnly.FromDateTime(DateTime.Now)).ToList();

        return experied.ToList();
    }

    public Ppe ReturnPpe(ObjectId epiId)
    {
        var epi = Ppes.First(epi => epi.InventoryId.Equals(epiId));

        Ppes.Remove(epi);

        return epi;
    }
}