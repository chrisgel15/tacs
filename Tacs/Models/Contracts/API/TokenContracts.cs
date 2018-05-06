using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web;

namespace Tacs.Models.Contracts.API
{
    [DataContract]
    public class TokenRequest
    {
        [DataMember, Required]
        public string username { get; set; }

        [DataMember, Required]
        public string password { get; set; }

        public TokenRequest(string u, string p)
        {
            this.username = u;
            this.password = p;
        }

        public FormUrlEncodedContent ToFormUrlEncodedContent()
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", this.username),
                new KeyValuePair<string, string>("password", this.password)
            });
            return content;
        }
    }

    [DataContract]
    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string token;

        [JsonProperty("token_type")]
        public string type;

        [JsonProperty("expires_in")]
        public int expiresIn;

    }
}