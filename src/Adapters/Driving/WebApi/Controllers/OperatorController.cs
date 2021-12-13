using Application.DataTransferObject;
using Application.Ports.DomainServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            await _operatorDomainService.RegisterOperatorAsync(registerOperator, cancellationToken);
            return Ok();
        }

    }
}
