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
    [RoutePrefix("api/transactions/coin/{coinId}")]
    public class TransactionsController : ApiController
    {
        [Route("")]
        [HttpGet]
        public HttpResponseMessage Transactions(int coinId)
        {
            TransactionService transactionService = new TransactionService();
            IList<Transaction> transactions = null;

            try
            {
                transactions = transactionService.GetTransactionsByCoinId(coinId);
            }
            catch (BusinnesException businessException)
            {
                return NotFoundResponse(businessException);
            }
            catch(Exception exception)
            {
                throw exception;
            }

            return SuccessfullResponse(coinId, transactions);
        }

        private HttpResponseMessage BadRequestResponse()
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Incorrect input");
        }
        private HttpResponseMessage SuccessfullResponse(int coinId, IList<Transaction> transactions)
        {
            return Request.CreateResponse<GetTransactionsByCoinResponse>(new GetTransactionsByCoinResponse(transactions)
            {
              CoinId = coinId,            
              Error = false
            });
        }
        private HttpResponseMessage NotFoundResponse(BusinnesException businnesException)
        {
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, businnesException);
        }

    }
}