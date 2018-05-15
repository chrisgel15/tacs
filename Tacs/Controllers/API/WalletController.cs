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

namespace Tacs.Controllers
{
    [RoutePrefix("api/users/{userId}/wallets")]
    public class CoinsController : ApiController
    {
        //Listar Wallets de un usuario
        [Authorize, Route("")]
        [HttpGet]
        public HttpResponseMessage GetWallets(int userId)
        {
            if (!ModelState.IsValid) return Request.CreateResponse(HttpStatusCode.BadRequest);
            if (new WalletService().VerPortfolio(userId).Count == 0) return Request.CreateResponse(HttpStatusCode.NotFound);
            return Request.CreateResponse(new WalletService().VerPortfolio(userId).Select(async w => await new WalletService().GetWalletInfo(w)));
        }

        //Listar detalles de un wallet (incluyendo cotizacion al momento)
        [Authorize, Route("{walletId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetWallet([FromUri]int userId, string walletId)
        {
            var wallet = new WalletService().GetWalletByCoinNameOrWalletIdAndUser(walletId, userId);
            if (wallet == null) return BadRequest("Wallet no encontrada");
            else return Ok<WalletViewModel>(await new WalletService().GetWalletInfo(wallet));
        }
        //Crear nuevo wallet para usuarioId
        [Authorize, Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> NewWallet([FromBody]NewWalletRequest newWalletRequest, int userId)
        {
            //TODO: validar que la moneda exista en CMC si el contract no valida
            var wallet = await new WalletService().AddWallet(newWalletRequest, userId);
            return Ok<WalletViewModel>(await new WalletService().GetWalletInfo(wallet));
        }


    }
}