﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Tacs.Models;

namespace Tacs.Controllers
{
    public class UsersController : ApiController
    {
        static HttpClient client = new HttpClient();
        
        // GET api/users
        public object Get()
        {
            // por ahora que devuelva estos datos
            var usersDB = new List<object>
            {
                new { id = 12355, username = "Pedro", coins = new List<string>{"bitcoin", "ethereum"} },
                new { id = 12355, username = "Matias", coins = new List<string>{"ethereum"} },
                new { id = 12355, username = "Brian", coins = new List<string>{"ethereum", "monero"} }
            };
            return Json(new { users = usersDB });
        }

        // GET api/users?userId=4565423
        //public object Get(int userId)
        public  async Task<String> Get(int userId)
        {
            // por ahora que devuelva estos datos
            // return Json(new { user = userId, username = "Matias", coins = new List<string> { "ethereum" } });

            string result =  await GetProductAsync("http://api.coinmarketcap.com/v1/ticker/bitcoin/");
            
            return result;
           
        }

        private  async Task<String> GetProductAsync(string path)
        {
            string result = null;
            var client = new HttpClient();
            client.BaseAddress = new Uri(path);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
             new MediaTypeWithQualityHeaderValue("application/json"));

            
            HttpResponseMessage response =  await client.GetAsync(new Uri(path));


            response.EnsureSuccessStatusCode();
             if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }


            return result;
               
        }
    }
}