using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tacs.Services;
using Tacs.Models.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.IO;
using System;
using Newtonsoft.Json;

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

        [Route(""), HttpGet]
        public IHttpActionResult ObtenerCotizaciones()
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://api.coinmarketcap.com/v1/ticker/"));

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            Console.WriteLine(WebResp.StatusCode);
            Console.WriteLine(WebResp.Server);

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())   //modified from your code since the using statement disposes the stream automatically when done
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            List<Ticker> items = JsonConvert.DeserializeObject<List<Ticker>>(jsonString);

            List<TickerResponse> response = items.Select(i => new TickerResponse(i)).ToList();

            return Ok<List<TickerResponse>>(response);
        }
    }

    public class Ticker
    {
        public string id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string rank { get; set; }
        public string price_usd { get; set; }
        [JsonProperty(PropertyName = "24h_volume_usd")]
        public string volume_usd_24h { get; set; }
        public string market_cap_usd { get; set; }
        public string available_supply { get; set; }
        public string total_supply { get; set; }
        public string percent_change_1h { get; set; }
        public string percent_change_24h { get; set; }
        public string percent_change_7d { get; set; }
        public string last_updated { get; set; }
    }

    public class TickerResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string rank { get; set; }
        public string price_usd { get; set; }
        public string last_updated { get; set; }

        public TickerResponse(Ticker ticker)
        {
            id = ticker.id;
            name = ticker.name;
            symbol = ticker.symbol;
            rank = ticker.rank;
            price_usd = ticker.price_usd;
            last_updated = ticker.last_updated;
        }
    }
}
