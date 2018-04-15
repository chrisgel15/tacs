using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Mvc;

namespace Tacs.Controllers
{
    [RoutePrefix("/api/transacciones")]
    public class TransactionsController : ApiController
    {
        // GET api/transactions?coinId=bitcoin
        public object Get(string coinId)
        {
            // por ahora que devuelva estos datos
            var transQuery = new List<object>
            {
                new { tradeType = "buy", amount = 0.06, price = 8995.65, date = DateTime.Now.AddMonths(-5) },
                new { tradeType = "sale", amount = 0.005, price = 8998.15, date = DateTime.Now.AddDays(-15) },
                new { tradeType = "buy", amount = 0.9, price = 8800, date = DateTime.Now.AddDays(-2) }
            };

            return Json(new { coin = coinId, transactions = transQuery });
        }

        [HttpGet]
        HttpResponseMessage transacciones()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}