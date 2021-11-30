using System;

namespace Application.DataTransferObject;

public record RegisterWorkerDto(string Name, string Password, DateTime BirthDate, string Nin);
