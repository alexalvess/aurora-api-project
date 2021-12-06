using MongoDB.Bson;
using System;

namespace Application.DataTransferObject;

public record RegisterOperatorDto(string Name, string Password, DateTime BirthDate, string Nin);

public record DistributeEpiDto(ObjectId epiId);
