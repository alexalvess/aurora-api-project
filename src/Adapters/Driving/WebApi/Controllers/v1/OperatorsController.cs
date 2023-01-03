using Application.DataTransferObject;
using Application.Envelop;
using Application.Extensions;
using Application.Ports.DomainServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using MongoDB.Bson;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1")]
    [ApiController, Route("api/v{version:apiVersion}/[controller]")]
    public class OperatorsController : ControllerBase
    {
        private readonly IOperatorDomainService _operatorDomainService;
        private Queryable _queryable;

        public OperatorsController(IOperatorDomainService operatorDomainService, Queryable queryable)
            => (_operatorDomainService, _queryable) = (operatorDomainService, queryable);

        [HttpPost]
        public async Task<IActionResult> RegisterOperatorAsync([FromBody] RegisterOperatorDto registerOperator, CancellationToken cancellationToken)
        {
            var operatorId = await _operatorDomainService.RegisterOperatorAsync(registerOperator, cancellationToken);
            return CreatedAtRoute(nameof(this.RecoverOperatorByIdAsync), routeValues: new { id = operatorId.ToString() }, null);
        }

        [HttpGet(Name = nameof(OperatorsController.RecoverOperatorsAsync))]
        public async Task<IActionResult> RecoverOperatorsAsync([FromQuery] Queryable queryable, CancellationToken cancellationToken)
        {
            _queryable.Bind(queryable);

            var operators = await _operatorDomainService.RetrieveOperatorsAsync(cancellationToken);
            return Ok(operators);
        }

        [HttpGet("{id}", Name = nameof(OperatorsController.RecoverOperatorByIdAsync))]
        public async Task<IActionResult> RecoverOperatorByIdAsync([FromRoute] string id, CancellationToken cancellationToken)
        {
            var operatorDetails = await _operatorDomainService.RetrieveOperatorDetailsAsync(id, cancellationToken);
            return Ok(operatorDetails);
        }
    }
}
