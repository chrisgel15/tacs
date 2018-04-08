using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Tacs.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/users
        public object Get()
        {
            // por ahora que devuelva estos datos
            var usersDB = new List<object>
            {
                new { id = 12355, username = "Pedro", coins = new List<string>{"bitcoin", "ethereum"} },
                new { id = 12355, username = "Matias", coins = new List<string>{"ethereum"} },
                new { id = 12355, username = "Brian", coins = new List<string>{"ethereum", "monero"} }
            };
            return Json(new { users = usersDB });
        }

        // GET api/users?userId=4565423
        public object Get(int userId)
        {
            // por ahora que devuelva estos datos
            return Json(new { user = userId, username = "Matias", coins = new List<string> { "ethereum" } });
        }
    }
}