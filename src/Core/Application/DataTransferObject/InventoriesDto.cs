using MongoDB.Bson;
using System;

namespace Application.DataTransferObject;

public record CreatePpeDto(string Name, string Description, DateOnly ManufacturingDate, long Durability, string ApprovalCertificate);

public record CreateInventoryDto(CreatePpeDto CreatePpe, long Quantity);

public record AddPpeDto(ObjectId InventoryId, long Quantity);