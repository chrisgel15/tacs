using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tacs.Models;

namespace Tacs.Controllers
{
    [RoutePrefix("/api/accesos")]
    public class SignupController : ApiController
    {
        // POST api/signup
        [Route("{userId}")]
        HttpResponseMessage nuevoUsuario(int userId)
        {
            var response = Request.CreateResponse(HttpStatusCode.Created);
            return response;
        }

        HttpResponseMessage accesoUsuario()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        public void Post([FromBody]User user)
        {

        }
    }
}
