using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tacs.Models;
using System.Web.Mvc;

namespace Tacs.Controllers
{
    public class BuyController : ApiController
    {
        // POST api/buy
        public object Post([FromBody]Trade trade)
        {
            return Json(new { coin = trade.CoinId, count = trade.Amount, transactionsId = 456481658 });
        }
    }
}
