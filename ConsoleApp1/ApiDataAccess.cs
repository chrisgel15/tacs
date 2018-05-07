using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TelegramBot.ViewModels;
using System.Globalization;

namespace TelegramBot
{
    public class ApiDataAccess
    {
        static string apiBaseUrl = "https://tacscripto.azurewebsites.net/api/";

        public async Task<UserWallet> GetWallet(int idUser)
        {
            var url = apiBaseUrl + "users/" + idUser + "/wallets";
            HttpClient client = CreateClient(url);

            Task<HttpResponseMessage> response = client.GetAsync(client.BaseAddress);

            string jsonResponse = await response.Result.Content.ReadAsStringAsync();

            UserWallet userWallet = new UserWallet();
            userWallet.coinWallets = new List<CoinWallet>();
            foreach (var jarray in JArray.Parse(jsonResponse))
            {
                CoinWallet coinWallet = jarray.SelectToken("Result").ToObject<CoinWallet>();
                userWallet.coinWallets.Add(coinWallet);
            }
         
            return userWallet;
        }

        public async void MakeTransaction(string transactionType, int idUser, string idCoin, Decimal amount)
        {
            var url = apiBaseUrl + "users/" + idUser + "/wallets/" + idCoin + "/transactions";
            HttpClient client = CreateClient(url);

            var values = new Dictionary<string, string>
            {
               { "type", transactionType },
               { "Amount", amount .ToString(CultureInfo.InvariantCulture)}
            };

            var content = new FormUrlEncodedContent(values);
            Task<HttpResponseMessage> response = client.PostAsync(client.BaseAddress, content);

            string jsonResponse = await response.Result.Content.ReadAsStringAsync();
        }

        public async Task<Decimal> GetCoinPrice(string coinId)
        {
            var url = "https://api.coinmarketcap.com/v1/ticker/" + coinId;
            HttpClient client = CreateClient(url);

            Task<HttpResponseMessage> response = client.GetAsync(client.BaseAddress);

            string jsonResponse = await response.Result.Content.ReadAsStringAsync();
            JArray jarray = JArray.Parse(jsonResponse);
            decimal cotizacion = (Decimal)jarray[0].SelectToken("price_usd");
            return cotizacion;
        }

        public HttpClient CreateClient(string url)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
             new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
