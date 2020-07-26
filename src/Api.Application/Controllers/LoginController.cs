using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
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

          [AllowAnonymous]
          [HttpPost]
          public async Task<object> Login([FromBody] LoginDto loginDto)
          {
               if(!ModelState.IsValid)
               {
                    return BadRequest(ModelState);
               }

               if(loginDto == null)
               {
                    return BadRequest();
               }

               try
               {
                    var result = await _loginService.FindByLogin(loginDto);

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
