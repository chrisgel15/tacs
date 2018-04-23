using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tacs.Models.Contracts;
using Tacs.Services;

namespace Tacs.Controllers
{
    [RoutePrefix("api/transaction")]
    public class BuyController : ApiController
    {
        [Route("")]
        public HttpResponseMessage Post([FromBody]TransactionRequest transactionRequest)
        {

            if (!ModelState.IsValid)
                return BadRequestResponse();

            var userId = transactionRequest.UserId;
            var coinId = transactionRequest.CoinId;
            var amount = transactionRequest.Amount;

            TransactionService transactionService = new TransactionService();

            try
            {
                transactionService.Buy(userId, coinId, amount);
            }
            catch(BusinnesException businessException)
            {
                return NotFoundResponse(businessException);
            }            
            catch(Exception exception)
            {
                // log error
                throw exception;
            }

            return SuccessfullBuyResponse();
        }

        private HttpResponseMessage BadRequestResponse()
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Incorrect input");
        }
        private HttpResponseMessage SuccessfullBuyResponse()
        {
            return Request.CreateResponse<BuyResponse>(new BuyResponse()
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
