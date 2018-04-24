using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tacs.Models.Contracts;
using Tacs.Services;

namespace Tacs.Controllers
{
    public class AdminController : ApiController
    {
        [Route("admin/reporte")]
        public HttpResponseMessage Post()
        {
            return Request.CreateResponse(new TransactionService().ListarTransacciones());
        }
    }
}