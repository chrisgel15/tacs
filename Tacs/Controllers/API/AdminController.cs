using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tacs.Models.Contracts;
using Tacs.Services;
using System.Linq;

namespace Tacs.Controllers
{
    [RoutePrefix("api/admin"), Authorize(Roles = "Admin")]
    public class AdminController : ApiController
    {
        TransactionService _transactionService;
        UserService _userService;
        AdminController(TransactionService transactionService, UserService userService)
        {
            _transactionService = transactionService;
            _userService = userService;
        }
        //Reporte de las transacciones totales del sistema (diarias, mensuales, etc)
        [Route("reporte"), HttpGet]
        public HttpResponseMessage GetTransacciones()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _transactionService.ListarTransacciones());
        }

        //Comparacion de balance total en dolares, de todas las wallets de dos usuarios
        [Route("compare"), HttpGet]
        public HttpResponseMessage GetComparacion([FromUri] string userName1, [FromUri] string userName2)
        {
            var user1 = _userService.GetUserByName(userName1);
            var user2 = _userService.GetUserByName(userName2);

            if (user1 == null || user2 == null) return Request.CreateResponse(HttpStatusCode.NotFound, "Usuario no encontrado");
            return Request.CreateResponse(HttpStatusCode.OK, _userService.GetUserComparisonResponse(user1, user2));
        }

        //Datos administrativos de un usuario
        [Route("users/{userId}"), HttpGet]
        public HttpResponseMessage GetUser(int userId)
        {
            var user = _userService.GetUserById(userId);

            if (user == null) return Request.CreateResponse(HttpStatusCode.NotFound, "Usuario no encontrado");
            return Request.CreateResponse(HttpStatusCode.OK, _userService.GetUserAdminInfo(userId));
        }
        [Route("users"), HttpGet]
        public HttpResponseMessage GetUsers()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _userService.GetUsers().Select(u => _userService.GetUserAdminInfo(u.Id)));
        }

        //Agregar un nuevo administrador
        [Route(""), HttpPost]
        public HttpResponseMessage Post([FromBody]SignupRequest user)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Campos incorrectos");

            bool estado = _userService.SignUp(user.Username, user.Password, true);
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