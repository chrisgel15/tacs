using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Tacs.Models;
using Tacs.Models.Contracts;
using Tacs.Services;
//using System.Web.Mvc;

namespace Tacs.Controllers
{
    [RoutePrefix("api/user/wallets/{walletId}/transactions")]
    public class TransactionsController : ApiController
    {
        CoinService _coinService;
        TransactionService _transactionService;
        UserService _userService;
        WalletService _walletService;
        public TransactionsController(CoinService coinService, TransactionService transactionService, UserService userService, WalletService walletService)
        {
            _coinService = coinService;
            _transactionService = transactionService;
            _userService = userService;
            _walletService = walletService;
        }

        //Comprar o vender
        [Authorize, Route(""), HttpPost]
        public HttpResponseMessage Post([FromBody]NewTransactionRequest transactionRequest, string walletId)
        {
            // Desde el Identity, recupero el Id del usuario
            int userId = TokenService.GetIdClient(User.Identity as ClaimsIdentity);
            // Obtengo el wallet por Id o por nombre de moneda (shortcut)
            var wallet = _walletService.GetWalletByCoinNameOrWalletIdAndUser(walletId, userId);

            if (!ModelState.IsValid) return Request.CreateResponse(HttpStatusCode.BadRequest, "Campos Incorrectos");
            //Si no se encontro el wallet y el request usa el shortcut de nombre de moneda existente en CMC, creo un nuevo wallet
            if (wallet == null && CoinService.ExisteEnCoinMarketCap(walletId))
            {
                Coin moneda = _coinService.GetCoinByName(walletId);
                User usuario = _userService.GetUserById(userId);
                wallet = _walletService.AddWallet(moneda, 0, usuario).Result;
            }

            if (wallet == null && walletId.All(c => Char.IsDigit(c))) return Request.CreateResponse(HttpStatusCode.NotFound, "Id de wallet inexistente");
            if (wallet == null && !walletId.All(c => Char.IsDigit(c))) return Request.CreateResponse(HttpStatusCode.NotFound, "Tipo de moneda inexistente");

            var type = transactionRequest.Type;
            var amount = transactionRequest.Amount;
            var transactionService = _transactionService;

            try
            {
                if (type.ToLower() == "compra") return Request.CreateResponse(HttpStatusCode.Created, new TransactionViewModel(transactionService.Buy(wallet.Id, amount)));
                else if (type.ToLower() == "venta") return Request.CreateResponse(HttpStatusCode.Created, new TransactionViewModel(transactionService.Sell(wallet.Id, amount)));
                else return Request.CreateResponse(HttpStatusCode.BadRequest, "La transaccion debe ser del tipo \"compra\" o \"venta\"");
            }

            catch (Exception exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, exception.Message); ;
            }
        }

        //Ver las transacciones de un wallet de un usuario
        [Authorize, Route(""), HttpGet]
        public HttpResponseMessage Get(string walletId)
        {
            // Desde el Identity, recupero el Id del usuario
            int userId = TokenService.GetIdClient(User.Identity as ClaimsIdentity);

            //var transactions = _walletService.GetWalletByCoinNameOrWalletIdAndUser(walletId, userId).Transactions;
            var wallet = _walletService.GetWalletByCoinNameOrWalletIdAndUser(walletId, userId);
            if (wallet == null && walletId.All(c => Char.IsDigit(c))) return Request.CreateResponse(HttpStatusCode.NotFound, "Id de wallet inexistente");
            if (wallet == null && !walletId.All(c => Char.IsDigit(c))) return Request.CreateResponse(HttpStatusCode.NotFound, "Tipo de moneda inexistente");
            else
            {
                var transactions = wallet.Transactions;
                var transactionsInfos = transactions.Select(t => _transactionService.GetTransactionInfo(t));
                return Request.CreateResponse<IList<TransactionViewModel>>(HttpStatusCode.OK ,transactionsInfos.ToList());
            }
            
        }

        //Ver detalles de una transaccion
        [Authorize, Route("{transactionId}"), HttpGet]
        public HttpResponseMessage GetById(int transactionId)
        {
            if (_transactionService.GetTransactionInfo(transactionId) == null) return Request.CreateResponse(HttpStatusCode.NotFound, "Id de transaccion inexistente");
            return Request.CreateResponse<TransactionViewModel>(HttpStatusCode.OK,_transactionService.GetTransactionInfo(transactionId));
        }
    }
}