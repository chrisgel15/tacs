using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Tacs.Models;
using Tacs.Models.Contracts.API;
using Tacs.Services;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Host.SystemWeb;
using System.Web.Http.Owin;


namespace Tacs.Controllers.API
{
    [RoutePrefix("api/token")]
    public class TokenController : ApiController
    {
        [AllowAnonymous, Route(""), HttpPost]
        public async Task<HttpResponseMessage> GetToken([FromBody]TokenRequest req)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Campos Incorrectos");

            req.password = System.Text.Encoding.Default.GetString(new SHA256CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(req.password)));

            var uri = Url.Content("~/") + "api/get_token";
            HttpResponseMessage response = await new HttpClient().PostAsync(uri, req.ToFormUrlEncodedContent());

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
                TokenResponse autentication = await response.Content.ReadAsAsync<TokenResponse>(new[] { new JsonMediaTypeFormatter() });
                return Request.CreateResponse(HttpStatusCode.OK, autentication);

            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "Credenciales Incorrectos");
            }
        }

        [Authorize, Route(""), HttpDelete]
        public HttpResponseMessage RemoveToken()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}