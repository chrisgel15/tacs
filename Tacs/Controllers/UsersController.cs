using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tacs.Services;
using Tacs.Models.Contracts;
using System.Collections.Generic;

namespace Tacs.Controllers
{
    [RoutePrefix("api/usuarios")]
    public class UsersController : ApiController
    {

        [Route(""), HttpGet]
        public IHttpActionResult Get()
        {
            return Ok<IList<UserInfoResponse>>(new UserService().GetUsers());
        }

        [Route("{userid}"), HttpGet]
        public IHttpActionResult GetById(int userId)
        {
            return Ok<UserInfoResponse>(new UserService().GetInfo(userId));
        }

        //llamar dos veces /users/{userid}? vienen los dos usuarios en objeto Request?
        [Route("~/api/comparacion")]
        [HttpGet]
        public HttpResponseMessage comparaDosUsuarios([FromBody] int userId, [FromBody] int otroUserId)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

    }
}