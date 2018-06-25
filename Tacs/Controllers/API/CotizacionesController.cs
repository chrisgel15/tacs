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
    /// <summary>
    /// Recurso publico que permite obtener cotizacion de una criptomoneda de CoinMarketCap.
    /// </summary>
    [RoutePrefix("api/cotizaciones")]
    public class CotizacionesController : ApiController
    {
        /// <summary>
        /// Obtiene el precio en dolares de una criptomoneda.
        /// </summary>
        /// <param name="nombreMoneda">El nombre de la criptomoneda, debe coincidir con el ticker de una moneda existente en CoinMarketCap.</param>
        [Route("{nombreMoneda}"), HttpGet]
        public IHttpActionResult ObtenerCotizacion(string nombreMoneda)
        {
            return Ok<decimal>(CoinService.GetCotizacion(nombreMoneda).Result);
        }
    }
}