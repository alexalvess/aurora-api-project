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
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerDomainService _workerDomainService;

        public WorkerController(IWorkerDomainService workerDomainService)
            => _workerDomainService = workerDomainService;

        [HttpPost]
        public async Task<IActionResult> RegisterWorkerAsync([FromBody] RegisterOperatorDto registerWorker, CancellationToken cancellationToken)
        {
            await _workerDomainService.RegisterWorkerAsync(registerWorker, cancellationToken);
            return Ok();
        }

    }
}
