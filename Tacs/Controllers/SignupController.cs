using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tacs.Models;
using Tacs.Models.DTO;

namespace Tacs.Controllers
{
    public class SignupController : ApiController
    {
        // POST api/signup
        public void Post([FromBody]SignupRequest request)
        {

        }
    }
}
