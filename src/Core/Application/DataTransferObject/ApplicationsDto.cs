using Domain.Enumerations;
using MongoDB.Bson;
using System;

namespace Application.DataTransferObject;

public record RegisterOperatorDto(string Name, string Password, DateTime BirthDate, string Nin);

public record DistributeEpiDto(ObjectId epiId);

public record RetrieveOperatorDetailsDto(string Name, DateTime BirthDate, string Nin, WorkShift WorkShift, bool IsActive, DateTime AdmissionDate);

public record RetrieveOperators(string Name, DateTime? BirthDate, string Nin, WorkShift? WorkShift);
