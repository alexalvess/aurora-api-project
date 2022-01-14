using Application.Abstractions.Pagination;
using Application.DataTransferObject;
using Application.Ports.DomainServices;
using Application.Ports.MongoServices;
using Application.Ports.NotificationServices;
using Domain;
using Domain.Aggregates.Employee.Operator;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DomainServices;

public class OperatorDomainService : IOperatorDomainService
{
    private readonly IOperatorService _operatorService;
    private readonly INotificationContext _notificationContext;

    public OperatorDomainService(IOperatorService operatorService, INotificationContext notificationContext)
        => (_operatorService, _notificationContext) = (operatorService, notificationContext);

    public async Task<ObjectId> RegisterOperatorAsync(RegisterOperatorDto registerOperatorDto, CancellationToken cancellationToken)
    {
        Operator @operator = new()
        {
            BirthDate = registerOperatorDto.BirthDate,
            Name = registerOperatorDto.Name,
            Nin = registerOperatorDto.Nin,
            Password = registerOperatorDto.Password
        };

        if (@operator.IsValid is false)
        {
            _notificationContext.AddNotifications(@operator.Errors);

            return default;
        }

        await _operatorService.SaveNewOperatorAsync(@operator, cancellationToken);

        return @operator.Id;
    }

    public async Task<RetrieveOperatorDetailsDto> RetrieveOperatorDetailsAsync(string operatorId, CancellationToken cancellationToken)
    {
        var @operator = await _operatorService.GetOperatorByIdAsync(new ObjectId(operatorId), cancellationToken);

        if (@operator is null)
        {
            _notificationContext.AddNotification(nameof(DomainErrorsResource.GetOperator_NotFound), DomainErrorsResource.GetOperator_NotFound);
            return default;
        }

        return new(
            @operator.Name.ToString(),
            @operator.BirthDate,
            @operator.Nin.ToString(),
            @operator.WorkShift,
            @operator.Active,
            @operator.AdmissionDate);
    }

    public async Task<IEnumerable<RetrieveOperators>> RetrieveOperatorsAsync(int limit, int offset, CancellationToken cancellationToken)
    {
        var paging = new Paging
        {
            Limit = limit,
            Offset = offset,
        };

        var operators = await _operatorService.GetAllOperators(paging, cancellationToken);

        if(!operators.Items?.Any() ?? true)
        {
            _notificationContext.AddNotification(nameof(DomainErrorsResource.GetOperators_NotFound), DomainErrorsResource.GetOperators_NotFound);
            return default;
        }

        return operators.Items.Select(@operator => 
            new RetrieveOperators(
                @operator.Name.ToString(), 
                @operator.BirthDate == default ? null : @operator.BirthDate, 
                @operator.Nin.ToString(), 
                @operator.WorkShift));
    }
}