using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo.Domain.Entities;
using Modelo.Service.Services;
using Modelo.Service.Validators;

namespace Modelo.Application.Controllers
{
    [Produces("application/json")]
    [Route("api/Usuario")]
    public class UsuarioController : Controller
    {
        private BaseService<Usuario> service = new BaseService<Usuario>();

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Usuario item)
        {
            try
            {
                if (item == null)
                    return NotFound();

                service.Add<UsuarioValidator>(item);

                return new ObjectResult(item.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Usuario item)
        {
            try
            {
                if (item == null)
                    return NotFound();

                service.Update<UsuarioValidator>(item);

                return new ObjectResult(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id == 0)
                    return NotFound();

                service.Delete(id);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return new ObjectResult(service.Get());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id == 0)
                    return NotFound();

                return new ObjectResult(service.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}