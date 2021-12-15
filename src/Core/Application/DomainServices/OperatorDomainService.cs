using Application.DataTransferObject;
using Application.Ports.DomainServices;
using Application.Ports.MongoServices;
using Domain.Aggregates.Employee.Operator;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DomainServices;

public class OperatorDomainService : IOperatorDomainService
{
    private readonly IOperatorService _operatorService;

    public OperatorDomainService(IOperatorService operatorService)
        => _operatorService = operatorService;

    public async Task<ObjectId> RegisterOperatorAsync(RegisterOperatorDto registerWorkerDto, CancellationToken cancellationToken)
    {
        Operator @operator = new()
        {
            BirthDate = DateOnly.FromDateTime(registerWorkerDto.BirthDate),
            Name = registerWorkerDto.Name,
            Nin = registerWorkerDto.Nin,
            Password = registerWorkerDto.Password
        };

        if (@operator.IsValid is false)
            return default;

        await _operatorService.SaveNewOperatorAsync(@operator, cancellationToken);

        return @operator.Id;
    }

    //public async Task DistributePpesAsync(IReadOnlyCollection<DistributeEpiDto> distributeEpisDto, CancellationToken cancellationToken)
    //{

    //}
}