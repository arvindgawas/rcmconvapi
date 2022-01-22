using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Features.Users.Queries;
using Application.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class UserController : BaseApiController
    {
        // GET api/<controller>/5
        [HttpPost]
        public async Task<IActionResult> AuthorizeUser(GetUserByIdQuery query)
        {
            var user = await Mediator.Send(query);

            if (user == null)
            {
                return Ok("");
            }

            return Ok(user);
        }


       
    }
}
