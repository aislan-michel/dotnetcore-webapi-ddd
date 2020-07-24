using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
     [Route("api/v1/[controller]")]
     [ApiController]
     public class UsersController : ControllerBase
     {
          [HttpGet("GetAllUsers")]
          public async Task<ActionResult> GetAllUsers([FromServices] IUserService service)
          {
               if(!ModelState.IsValid)
               {
                    return BadRequest(ModelState);
               }

               try
               {
                    return Ok(await service.GetAllUsers());
               }
               catch(ArgumentException argumentException)
               {
                    return StatusCode((int) HttpStatusCode.InternalServerError, argumentException.Message);
               }
          }
     }
}
