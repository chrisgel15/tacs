using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Tacs.Services
{
    public class CotizacionService 
    {

        public static decimal GetCotizacion(String nombreMoneda)
        {
            return CotizacionService.TraerCotizacion("https://api.coinmarketcap.com/v1/ticker/" + nombreMoneda);
        }


        private static decimal TraerCotizacion(String path)
        {

            var client = CrearHttpClient();

            InitHttpClient(client, path);

            Task<String> result = ConsultarAPiExterna(client, path);


            return Cotizacion(result.Result);
        }

        private static Task<string> ConsultarAPiExterna(HttpClient client, string path)
        {
            Task<HttpResponseMessage> response = client.GetAsync(path);
            Task<string> result = response.Result.Content.ReadAsStringAsync();
            return result;
        }

        private static HttpClient CrearHttpClient()
        {
            HttpClient client = new HttpClient();
            return client;

        }

        private static void InitHttpClient(HttpClient client, String path)
        {
            client.BaseAddress = new Uri(path);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
             new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static decimal Cotizacion(string result)
        {
            JArray jarray = JArray.Parse(result);
            string _scotizacion = (string)jarray[0].SelectToken("price_usd");
            decimal cotizacion = Decimal.Parse(_scotizacion, System.Globalization.NumberStyles.Any);
            return cotizacion;
        }
    }
}