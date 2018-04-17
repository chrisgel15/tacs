using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tacs.Context;
using Tacs.Models;
using Tacs.Models.DTO;
using Tacs.Models.Repositories;

namespace Tacs.Controllers
{
    public class SignupController : ApiController
    {
        // POST api/signup
        public void Post([FromBody]SignupRequest request)
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
