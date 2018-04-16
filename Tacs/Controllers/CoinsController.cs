using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Mvc;

namespace Tacs.Controllers
{
    [RoutePrefix("api/monedas")]
    public class CoinsController : ApiController
    {
        // GET api/coins?coinId=bitcoin
        public object Get(string coinId)
        {
            // por ahora que devuelva estos datos
            return Json(new { coin = coinId, price = 10000 });
        }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage monedas()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        [Route("{monedaid}/cotizacion")]
        [HttpGet]
        public HttpResponseMessage GetCotizacion(int monedaid)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

    }
}