using Application.DataTransferObject;
using Application.Ports.DomainServices;
using Application.Ports.MongoServices;
using Domain.Aggregates.Employee.Operator;
using Domain.ValueObjects.Epis;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DomainServices;

public class OperatorDomainService : IWorkerDomainService
{
    private readonly IOperatorService _workerService;

    public OperatorDomainService(IOperatorService workerService)
        => _workerService = workerService;

    public async Task RegisterWorkerAsync(RegisterOperatorDto registerWorkerDto, CancellationToken cancellationToken)
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

    public async Task DistributeEpisAsync(IReadOnlyCollection<DistributeEpiDto> distributeEpisDto, CancellationToken cancellationToken)
    {

    }
}