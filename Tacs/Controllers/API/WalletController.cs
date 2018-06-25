using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Tacs.Services;
using Tacs.Models;
using Tacs.Models.Contracts;
using System.Runtime.Serialization.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace Tacs.Controllers
{
    [RoutePrefix("api/user/wallets")]
    public class WalletController : ApiController
    {
        //Listar Wallets de un usuario
        [Authorize, Route(""), HttpGet]
        public HttpResponseMessage GetWallets()
        {
            // Desde el Identity, recupero el Id del usuario
            int userId = TokenService.GetIdClient(User.Identity as ClaimsIdentity);

            if (!ModelState.IsValid) return Request.CreateResponse(HttpStatusCode.BadRequest, "Campos Invalidos");
            //if (new WalletService().VerPortfolio(userId).Count == 0) return Request.CreateResponse(HttpStatusCode.NotFound, "El usuario no tiene wallets");
            return Request.CreateResponse(HttpStatusCode.OK,new WalletService().VerPortfolio(userId).Select(w => new WalletService().GetWalletInfo(w).Result));
        }

        //Listar detalles de un wallet (incluyendo cotizacion al momento)
        [Authorize, Route("{walletId}"), HttpGet]
        public async Task<HttpResponseMessage> GetWallet(string walletId)
        {
            // Desde el Identity, recupero el Id del usuario
            int userId = TokenService.GetIdClient(User.Identity as ClaimsIdentity);

            var wallet = new WalletService().GetWalletByCoinNameOrWalletIdAndUser(walletId, userId);
            if (wallet == null) return Request.CreateResponse(HttpStatusCode.NotFound,"Wallet no encontrada");
            else return Request.CreateResponse<WalletViewModel>(HttpStatusCode.OK ,await new WalletService().GetWalletInfo(wallet));
        }
        //Crear nuevo wallet para usuarioId
        [Authorize, Route(""), HttpPost]
        public async Task<HttpResponseMessage> NewWallet([FromBody]NewWalletRequest newWalletRequest)
        {
            if (!ModelState.IsValid) return Request.CreateResponse(HttpStatusCode.BadRequest, "Campos Incorrectos");
            // Desde el Identity, recupero el Id del usuario
            int userId = TokenService.GetIdClient(User.Identity as ClaimsIdentity);

            if (!CoinService.ExisteEnCoinMarketCap(newWalletRequest.NombreMoneda)) return Request.CreateResponse(HttpStatusCode.Forbidden, "La moneda debe existir en CoinMarketCap");
            if (newWalletRequest.Balance < 0) return Request.CreateResponse(HttpStatusCode.Forbidden, "El balance no puede ser negativo");
            Coin moneda = new CoinService().GetCoinByName(newWalletRequest.NombreMoneda);
            User usuario = new UserService().GetUserById(userId);
            var wallet = await new WalletService().AddWallet(moneda, newWalletRequest.Balance, usuario);
            return Request.CreateResponse<WalletViewModel>(HttpStatusCode.Created, await new WalletService().GetWalletInfo(wallet));
        }


    }
}