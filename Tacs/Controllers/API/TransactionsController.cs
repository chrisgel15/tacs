using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tacs.Models;
using Tacs.Models.Contracts;
using Tacs.Services;
//using System.Web.Mvc;

namespace Tacs.Controllers
{
    [RoutePrefix("api/users/{userId}/wallets/{walletId}/transactions")]
    public class TransactionsController : ApiController
    {
        //Comprar o vender
        [Authorize, Route(""), HttpPost]
        public HttpResponseMessage Post([FromBody]NewTransactionRequest transactionRequest, int userId, string walletId)
        {
            var wallet = new WalletService().GetWalletByCoinNameOrWalletIdAndUser(walletId, userId);

            if (!ModelState.IsValid) return BadRequestResponse();
            if (wallet == null) return BadRequestResponse();

            var type = transactionRequest.Type;
            var amount = transactionRequest.Amount;
            var transactionService = new TransactionService();

            try
            {
                if (type.ToLower() == "compra") transactionService.Buy(wallet.Id, amount);
                else if (type.ToLower() == "venta") transactionService.Sell(wallet.Id, amount);
                else return BadRequestResponse();
            }

            catch (BusinnesException businessException)
            {
                return NotFoundResponse(businessException);
            }
            catch (Exception exception)
            {
                // log error
                throw exception;
            }

            return SuccessfullTransactionResponse();
        }

        //Ver las transacciones de un wallet de un usuario
        [Authorize, Route(""), HttpGet]
        public IHttpActionResult Get(int userId, string walletId)
        {
            //var transactions = new WalletService().GetWalletByCoinNameOrWalletIdAndUser(walletId, userId).Transactions;
            var wallet = new WalletService().GetWalletByCoinNameOrWalletIdAndUser(walletId, userId);
            if (wallet == null)
            {
                return NotFound();
            }
            else
            {
                var transactions = wallet.Transactions;
                var transactionsInfos = transactions.Select(t => new TransactionService().GetTransactionInfo(t));
                return Ok<IList<TransactionViewModel>>(transactionsInfos.ToList());
            }
            
        }

        //Ver detalles de una transaccion
        [Authorize, Route("{transactionId}"), HttpGet]
        public IHttpActionResult GetById(int transactionId)
        {
            return Ok<TransactionViewModel>(new TransactionService().GetTransactionInfo(transactionId));
        }

        private HttpResponseMessage BadRequestResponse()
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Incorrect input");
        }
        private HttpResponseMessage SuccessfullTransactionResponse()
        {
            return Request.CreateResponse<NewTransactionResponse>(new NewTransactionResponse()
            {
                Error = false,
                Message = "La compra ha sido exitosa."
            });
        }
        private HttpResponseMessage NotFoundResponse(BusinnesException businnesException)
        {
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, businnesException);
        }

    }
}