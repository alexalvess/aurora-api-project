using Application.DataTransferObject;
using Application.Ports.DomainServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using MongoDB.Bson;
using System;
using System.Threading;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IActionResult> RecoverOperatorsAsync()
        {
            return Ok();
        }

        [HttpGet("{id}", Name = nameof(OperatorController.RecoverOperatorByIdAsync))]
        public async Task<IActionResult> RecoverOperatorByIdAsync([FromRoute] string id)
        {
            return Ok();
        }
    }
}
