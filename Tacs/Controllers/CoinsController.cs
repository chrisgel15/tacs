﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
//using System.Web.Mvc;

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
        public async Task<IHttpActionResult> GetCotizacion([FromUri]string monedaid)
        {
            if (monedaid == "")
            {
                return BadRequest("No se encontraron los parametros de la Uri.");
            }
            else
            {
                HttpResponseMessage response = await new HttpClient().GetAsync("https://api.coinmarketcap.com/v1/ticker/" + monedaid);
                var result = await response.Content.ReadAsStringAsync();
                return Ok(JsonConvert.DeserializeObject(result));
            }
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
        public HttpResponseMessage getMonedasConCotizacionActual()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
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