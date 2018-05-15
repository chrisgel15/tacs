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

        //Reporte de las transacciones totales del sistema (diarias, mensuales, etc)
        [Authorize(Roles = "Admin"), Route("api/admin/reporte"), HttpGet]
        public HttpResponseMessage GetTransacciones()
        {
            return Request.CreateResponse(new TransactionService().ListarTransacciones());
        }

        //Comparacion de balance total en dolares, de todas las wallets de dos usuarios
        [Authorize(Roles = "Admin"), Route("api/admin/compare"), HttpGet]
        public HttpResponseMessage GetComparacion([FromUri] string userName1, [FromUri] string userName2)
        {
            var user1 = new UserService().GetUserByName(userName1);
            var user2 = new UserService().GetUserByName(userName2);

            return Request.CreateResponse(new UserService().GetUserComparisonResponse(user1, user2));
        }

        //Datos administrativos de un usuario
        [Authorize(Roles = "Admin"), Route("api/admin/users/{userId}"), HttpGet]
        public HttpResponseMessage GetUser(int userId)
        {
            return Request.CreateResponse(new UserService().GetUserAdminInfo(userId));
        }

    }
}