using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tacs.Models;
using Tacs.Models.Repositories;

namespace Tacs.Services
{
    public class CoinService 
    {

        public static bool ExisteEnCoinMarketCap(string coinName)
        {
            var client = new HttpClient();
            if (client.GetAsync("https://api.coinmarketcap.com/v1/ticker/" + coinName).Result.StatusCode == HttpStatusCode.OK) return true;
            else return false;
        }

        public static int GetCoinId(string coinName)
        {
            //verificar que existe en CMC antes de crearla si no existe en nuestra app
            var coin = new UnitOfWork(new Context.TacsDataContext()).Coins.Find(c => c.Name.ToLower() == coinName.ToLower()).FirstOrDefault();
            if (coin == null) return AddCoin(coinName);
            else return coin.Id;
        }

        public static int AddCoin(string coinName)
        {
            var contexto = new UnitOfWork(new Context.TacsDataContext());
            contexto.Coins.Add(new Coin(coinName));
            contexto.Complete();
            return GetCoinId(coinName);
        }


        public async static Task<decimal> GetCotizacion(String nombreMoneda)
        {
            return await TraerCotizacion("https://api.coinmarketcap.com/v1/ticker/" + nombreMoneda);
        }


        private async static Task<decimal> TraerCotizacion(String path)
        {
            var client = CrearHttpClient();

            InitHttpClient(client, path);

            return Cotizacion(await ConsultarAPiExterna(client, path));
        }

        private async static Task<string> ConsultarAPiExterna(HttpClient client, string path)
        {
            Task<HttpResponseMessage> response = client.GetAsync(path);
            return await response.Result.Content.ReadAsStringAsync();
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
            decimal cotizacion = Decimal.Parse(_scotizacion, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture);
            return cotizacion;
        }
    }
}