using Domain.Abstractions.Aggregates;
using MongoDB.Bson;
using System;

namespace Domain.Aggregates.Employee;

public interface IEmployee : IAggregateRoot<ObjectId> { }