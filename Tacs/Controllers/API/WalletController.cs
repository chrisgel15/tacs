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
        CoinService _coinService;
        UserService _userService;
        WalletService _walletService;
        WalletController(CoinService coinService, UserService userService, WalletService walletService)
        {
            _coinService = coinService;
            _userService = userService;
            _walletService = walletService;
        }
        //Listar Wallets de un usuario
        [Authorize, Route(""), HttpGet]
        public HttpResponseMessage GetWallets()
        {
            // Desde el Identity, recupero el Id del usuario
            int userId = TokenService.GetIdClient(User.Identity as ClaimsIdentity);

            if (!ModelState.IsValid) return Request.CreateResponse(HttpStatusCode.BadRequest, "Campos Invalidos");
            return Request.CreateResponse(HttpStatusCode.OK,_walletService.VerPortfolio(userId).Select(w => _walletService.GetWalletInfo(w).Result));
        }

        //Listar detalles de un wallet (incluyendo cotizacion al momento)
        [Authorize, Route("{walletId}"), HttpGet]
        public async Task<HttpResponseMessage> GetWallet(string walletId)
        {
            // Desde el Identity, recupero el Id del usuario
            int userId = TokenService.GetIdClient(User.Identity as ClaimsIdentity);

            var wallet = _walletService.GetWalletByCoinNameOrWalletIdAndUser(walletId, userId);
            if (wallet == null) return Request.CreateResponse(HttpStatusCode.NotFound,"Wallet no encontrada");
            else return Request.CreateResponse<WalletViewModel>(HttpStatusCode.OK ,await _walletService.GetWalletInfo(wallet));
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
            Coin moneda = _coinService.GetCoinByName(newWalletRequest.NombreMoneda);
            User usuario = _userService.GetUserById(userId);
            var wallet = await _walletService.AddWallet(moneda, newWalletRequest.Balance, usuario);
            return Request.CreateResponse<WalletViewModel>(HttpStatusCode.Created, await _walletService.GetWalletInfo(wallet));
        }


    }
}