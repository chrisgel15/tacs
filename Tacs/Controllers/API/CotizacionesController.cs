using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tacs.Services;
using Tacs.Models.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Tacs.Controllers
{
    [RoutePrefix("api/cotizaciones")]
    public class MonedaController : ApiController
    {
        [Route("{nombreMoneda}"), HttpGet]
        public IHttpActionResult ObtenerCotizacion(string nombreMoneda)
        {
            return Ok<decimal>(CoinService.GetCotizacion(nombreMoneda).Result);
        }
    }
}