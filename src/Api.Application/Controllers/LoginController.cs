using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
     [Route("api/v1/[controller]")]
     [ApiController]
     public class LoginController : ControllerBase
     {
          private readonly ILoginService _loginService;

          public LoginController(ILoginService loginService)
          {
              _loginService = loginService;
          }

          [HttpPost]
          public async Task<object> Login([FromBody] UserEntity entity)
          {
               if(!ModelState.IsValid)
               {
                    return BadRequest(ModelState);
               }

               if(entity == null)
               {
                    return BadRequest();
               }

               try
               {
                    var result = await _loginService.FindByLogin(entity);

                    if(result != null)
                    {
                         return Ok(result);
                    }

                    return NotFound();
               }
               catch(ArgumentException argumentException)
               {
                    return StatusCode((int)HttpStatusCode.InternalServerError, argumentException.Message);
               }
          }

     }
}
