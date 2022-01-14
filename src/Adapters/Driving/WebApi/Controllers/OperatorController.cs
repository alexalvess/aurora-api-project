using Application.DataTransferObject;
using Application.Ports.DomainServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatorController : ControllerBase
    {
        private readonly IOperatorDomainService _operatorDomainService;

        public OperatorController(IOperatorDomainService operatorDomainService)
            => _operatorDomainService = operatorDomainService;

        [HttpPost]
        public async Task<IActionResult> RegisterOperatorAsync([FromBody] RegisterOperatorDto registerOperator, CancellationToken cancellationToken)
        {
            var operatorId = await _operatorDomainService.RegisterOperatorAsync(registerOperator, cancellationToken);
            return CreatedAtRoute(nameof(this.RecoverOperatorByIdAsync), routeValues: new { id = operatorId.ToString() }, null);
        }

        [HttpGet(Name = nameof(OperatorController.RecoverOperatorsAsync))]
        [QueryableFilter]
        public async Task<IActionResult> RecoverOperatorsAsync([FromQuery] int limit, [FromQuery] int offset, CancellationToken cancellationToken)
        {
            var operators = await _operatorDomainService.RetrieveOperatorsAsync(limit, offset, cancellationToken);
            return Ok(operators);
        }

        [HttpGet("{id}", Name = nameof(OperatorController.RecoverOperatorByIdAsync))]
        public async Task<IActionResult> RecoverOperatorByIdAsync([FromRoute] string id, CancellationToken cancellationToken)
        {
            var operatorDetails = await _operatorDomainService.RetrieveOperatorDetailsAsync(id, cancellationToken);
            return Ok(operatorDetails);
        }
    }
}
