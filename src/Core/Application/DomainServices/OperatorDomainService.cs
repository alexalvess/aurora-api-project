using Application.DataTransferObject;
using Application.Ports.DomainServices;
using Application.Ports.MongoServices;
using Domain.Aggregates.Employee.Operator;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DomainServices;

public class OperatorDomainService : IOperatorDomainService
{
    private readonly IOperatorService _workerService;

    public OperatorDomainService(IOperatorService workerService)
        => _workerService = workerService;

    public async Task RegisterOperatorAsync(RegisterOperatorDto registerWorkerDto, CancellationToken cancellationToken)
    {
        Operator @operator = new()
        {
            BirthDate = DateOnly.FromDateTime(registerWorkerDto.BirthDate),
            Name = registerWorkerDto.Name,
            Nin = registerWorkerDto.Nin,
            Password = registerWorkerDto.Password
        };

        if (@operator.IsValid is false)
            return;

        await _workerService.SaveNewOperatorAsync(@operator, cancellationToken);
    }

    //public async Task DistributePpesAsync(IReadOnlyCollection<DistributeEpiDto> distributeEpisDto, CancellationToken cancellationToken)
    //{

    //}
}