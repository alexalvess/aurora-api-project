using System;
using Aurora.Domain.Entities;
using Aurora.Service.Services;
using Aurora.Service.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Application.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private BaseService<User> service = new BaseService<User>();

        [HttpPost]
        public IActionResult Register([FromBody] User item)
        {
            try
            {
                service.Insert<UserValidator>(item);

                return Ok(item.Id);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] User item)
        {
            try
            {
                service.Update<UserValidator>(item);

                return Ok(item);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
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
                service.Delete(id);

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex);
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
                return Ok(service.Browser());
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
                return Ok(service.Recover(id));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}