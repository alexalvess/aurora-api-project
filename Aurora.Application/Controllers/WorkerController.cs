using System;
using Aurora.Domain.Interfaces;
using Aurora.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Application.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class WorkerController : Controller
    {
        private readonly IServiceWorker _serviceUser;

        public WorkerController(IServiceWorker serviceUser) =>
            _serviceUser = serviceUser;


        [HttpPost]
        public IActionResult Register([FromBody] CreateWorkerModel userModel)
        {
            try
            {
                var user = _serviceUser.Insert(userModel);

                return Created($"/api/users/{user?.Id}", user?.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateWorkerModel userModel)
        {
            try
            {
                var user = _serviceUser.Update(id, userModel);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove([FromRoute] int id)
        {
            try
            {
                _serviceUser.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public IActionResult RecoverAll()
        {
            try
            {
                var users = _serviceUser.RecoverAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Recover([FromRoute] int id)
        {
            try
            {
                var user = _serviceUser.RecoverById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}