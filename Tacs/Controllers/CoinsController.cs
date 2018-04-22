using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Mvc;
using Tacs.Services;
using Tacs.Models;
using Tacs.Models.Contracts;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace Tacs.Controllers
{
    [RoutePrefix("api/monedas")]
    public class CoinsController : ApiController
    {
        /*// GET api/coins?coinId=bitcoin
        public object Get(string coinId)
        {
            // por ahora que devuelva estos datos
            return Json(new { coin = coinId, price = 10000 });
        }*/

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

        
        [Route("~/api/usuarios/{userid}/portfolio")]
        [HttpPut]
        public HttpResponseMessage compraVentaMoneda([FromBody] string operacion, [FromBody] string moneda, [FromBody] int cantidad)
        {
            //Validaciones necesarias//
            var response = Request.CreateResponse(HttpStatusCode.Created);
            return response;
        }


        [Route("~/api/usuarios/{userid}/portfolio")]
        [HttpGet]
        public HttpResponseMessage GetMonedasConCotizacionActual(int userid)
        {

            List<PortfolioResponse> listaMonedas= new List<PortfolioResponse>();
            
            if (!ModelState.IsValid)
                return Request.CreateResponse( HttpStatusCode.BadRequest);

            CoinService coinService = new CoinService();

            var wallets = coinService.VerPortfolio(userid);

            //La siguiente linea solo para probar el cliente http
            //var cotizacion = TraerCotizacion("https://api.coinmarketcap.com/v1/ticker/bitcoin");

            //itera sobre todos los wallet que se encontro para el usuario con userid en la ruta
            foreach(Wallet w in wallets)
            {
                var cotizacion = TraerCotizacion("https://api.coinmarketcap.com/v1/ticker/" + w.Name.ToString());
                listaMonedas.Add(new PortfolioResponse(w.Name, cotizacion));
            }

            if (!(listaMonedas.Count > 0))
              return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse<IList<PortfolioResponse>>(HttpStatusCode.OK, listaMonedas);
            
            //la siguiente linea es solo para probar el objeto PortfolioResponse devuelto como Json
            //return Request.CreateResponse<PortfolioResponse>(HttpStatusCode.OK, new PortfolioResponse("Bitcoin", cotizacion));
        }

        private  decimal TraerCotizacion(String path)
        {

            var client = CrearHttpClient();

            InitHttpClient(client, path);

            Task<String> result = ConsultarAPiExterna(client, path);
            

            return Cotizacion(result.Result);
         }

        private Task<string> ConsultarAPiExterna(HttpClient client, string path)
        {
            Task<HttpResponseMessage> response = client.GetAsync(path);
            Task<string> result = response.Result.Content.ReadAsStringAsync();
            return result;
        }

        private HttpClient CrearHttpClient()
        {
            HttpClient client = new HttpClient();
            return client;

        }

        private void InitHttpClient(HttpClient client, String path)
        {
            client.BaseAddress = new Uri(path);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
             new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private decimal Cotizacion(string result)
        {
            JArray jarray = JArray.Parse(result);
            string _scotizacion = (string)jarray[0].SelectToken("price_usd");
            decimal cotizacion = Decimal.Parse(_scotizacion, System.Globalization.NumberStyles.Any);
            return cotizacion;
        }

       

        [Route("~/api/usuarios/{userid}/portfolio/{monedaid}/transacciones")]
        [HttpGet]
        public HttpResponseMessage detallesTransacciones(int monedaid)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        
        [Route("~/api/usuarios/{userid}/portfolio/{monedaid}")]
        [HttpGet]
        public HttpResponseMessage cantidadMoneda(int monedaid)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        
    }
}