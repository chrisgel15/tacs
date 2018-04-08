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
    public class WalletController : ApiController
    {
        // GET api/wallet
        public object Get()
        {
            // por ahora que devuelva estos datos
            var walletContent = new List<object>
            {
                new { coinId = "bitcoin", amount = 1.3, purchasePrice = 9870, salePrice = 8657 },
                new { coinId = "ethereum", amount = 150, purchasePrice = 385, salePrice = 399 },
                new { coinId = "monero", amount = 60.06, purchasePrice = 180, salePrice = 178.5 }
            };

            return Json(new { wallet = walletContent });
        }

        // GET api/wallet?coinId=bitcoin
        public object Get(string coinId)
        {
            // por ahora que devuelva estos datos
            return Json(new { coin = coinId, amount = 2.598 });
        }
    }
}