using Application.DataTransferObject;
using Application.Ports.DomainServices;
using Application.Ports.MongoServices;
using Domain.Entities.Workers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DomainServices;

public class WorkerDomainService : IWorkerDomainService
{
    private readonly IWorkerService _workerService;

    public WorkerDomainService(IWorkerService workerService)
        => _workerService = workerService;

    public async Task RegisterWorkerAsync(RegisterWorkerDto registerWorkerDto, CancellationToken cancellationToken)
    {
        Worker worker = new()
        {
            BirthDate = DateOnly.FromDateTime(registerWorkerDto.BirthDate),
            Name = registerWorkerDto.Name,
            Nin = registerWorkerDto.Nin,
            Password = registerWorkerDto.Password
        };

        if (worker.IsValid is false)
            return;

        await _workerService.SaveNewWorkerAsync(worker, cancellationToken);
    }
}