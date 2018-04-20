using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tacs.Context;
using Tacs.Models;
using Tacs.Models.Contracts;
using Tacs.Models.Repositories;

namespace Tacs.Controllers
{
    [RoutePrefix("api/accesos")]
    public class SignupController : ApiController
    {
        // POST api/signup
        [Route("")]
        [HttpPost]
        public HttpResponseMessage nuevoUsuario()
        {
            var response = Request.CreateResponse(HttpStatusCode.Created);
            return response;
        }

        [Route("{userId}")]
        [HttpGet]
        public HttpResponseMessage accesoUsuario(int userId)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        public void Post([FromBody]User user)
        {
            using (var unitOfWork = new UnitOfWork(new TacsDataContext()))
            {
                Coin c = new Coin("dCoin");

                unitOfWork.Coins.Add(c);

                User user = new User("asdf", "asdf");

                user.Buy(c, 500);

                unitOfWork.Users.Add(user);

                unitOfWork.Complete();
            }

        }
    }
}
