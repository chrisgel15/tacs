using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tacs.Models.Contracts;
using Tacs.Services;

namespace Tacs.Controllers
{
    [RoutePrefix("api/admin"), Authorize(Roles = "Admin")]
    public class AdminController : ApiController
    {
        //Reporte de las transacciones totales del sistema (diarias, mensuales, etc)
        [Route("api/admin/reporte"), HttpGet]
        public HttpResponseMessage GetTransacciones()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new TransactionService().ListarTransacciones());
        }

        //Comparacion de balance total en dolares, de todas las wallets de dos usuarios
        [Route("api/admin/compare"), HttpGet]
        public HttpResponseMessage GetComparacion([FromUri] string userName1, [FromUri] string userName2)
        {
            var user1 = new UserService().GetUserByName(userName1);
            var user2 = new UserService().GetUserByName(userName2);

            if (user1 == null || user2 == null) return Request.CreateResponse(HttpStatusCode.NotFound, "Usuario no encontrado");
            return Request.CreateResponse(HttpStatusCode.OK, new UserService().GetUserComparisonResponse(user1, user2));
        }

        //Datos administrativos de un usuario
        [Route("api/admin/users/{userId}"), HttpGet]
        public HttpResponseMessage GetUser(int userId)
        {
            var user = new UserService().GetUserById(userId);

            if (user == null) return Request.CreateResponse(HttpStatusCode.NotFound, "Usuario no encontrado");
            return Request.CreateResponse(HttpStatusCode.OK, new UserService().GetUserAdminInfo(userId));
        }

        //Agregar un nuevo administrador
        [Route(""), HttpPost]
        public HttpResponseMessage Post([FromBody]SignupRequest user)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Campos incorrectos");

            bool estado = new UserService().SignUp(user.Username, user.Password, true);
            if (estado)
            {
                return Request.CreateResponse(HttpStatusCode.Created); 
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "El usuario ya existe");
            }
        }

    }
}