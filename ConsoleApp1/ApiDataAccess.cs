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
        //TODO: tratar mejor los errores

        //TODO: ver si hay alguna forma de especificar el puerto en el cual se ejecuta el otro proyecto
        //Desarrollo
        static string apiBaseUrl = "http://localhost:51882/api/";
        //Produccion
        //static string apiBaseUrl = "https://tacscripto.azurewebsites.net/api/";

        public UserWallet GetWallet(string token)
        {
            var url = apiBaseUrl + "user/" + "wallets";
            HttpClient client = CreateClient(url);
            client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
            HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;

            string jsonResponse = response.Content.ReadAsStringAsync().Result;

            UserWallet userWallet = new UserWallet();
            userWallet.coinWallets = new List<CoinWallet>();
            if(jsonResponse != "")
            {
                foreach (var jarray in JArray.Parse(jsonResponse))
                {
                    CoinWallet coinWallet = jarray.SelectToken("Result").ToObject<CoinWallet>();
                    userWallet.coinWallets.Add(coinWallet);
                }
            }        
            return userWallet;
        }

        public string Login(string username, string password)
        {
            var url = apiBaseUrl + "token";
            HttpClient client = CreateClient(url);

            
            TokenRequest request = new TokenRequest { username = username, password = password };
            var myContent = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress, byteContent).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string jsonResponse = response.Content.ReadAsStringAsync().Result;
                var jObject = JObject.Parse(jsonResponse);
                string token = (string)jObject["access_token"];
                return token;
            }
            else
                return null;   
        }

        public void MakeTransaction(string transactionType, string token, string idCoin, Decimal amount)
        {
            var url = apiBaseUrl + "user" + "/wallets/" + idCoin + "/transactions";
            HttpClient client = CreateClient(url);
            client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
            var values = new Dictionary<string, string>
            {
               { "type", transactionType },
               { "Amount", amount .ToString(CultureInfo.InvariantCulture)}
            };

            var content = new FormUrlEncodedContent(values);
            HttpResponseMessage response = client.PostAsync(client.BaseAddress, content).Result;
            
            if(!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }
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

        private HttpClient CreateClient(string url)
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
