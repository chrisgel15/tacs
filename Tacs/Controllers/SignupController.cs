using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tacs.Context;
using Tacs.Models;
using Tacs.Models.Contracts;
using Tacs.Services;

namespace Tacs.Controllers
{
    [RoutePrefix("api/accesos")]
    public class SignupController : ApiController
    {
        [Route(""), HttpPost]
        public IHttpActionResult Post([FromBody]SignupRequest user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Campos incorrectos");

            var responseService = new UserService().SignUp(user.Username, user.Password);
            if (responseService.estado == "ERROR") {
                return Ok(new { responseService });
            }
            //var response = Request.CreateResponse(HttpStatusCode.Created);
            return Created("", responseService);
        }

        [Route("{userId}")]
        [HttpGet]
        public HttpResponseMessage accesoUsuario(int userId)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}
