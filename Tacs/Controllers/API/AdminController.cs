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
    /// <summary>
    /// Recursos disponibles para usuarios con privilegios de admin.
    /// </summary>
    [RoutePrefix("api/admin"), Authorize(Roles = "Admin")]
    public class AdminController : ApiController
    {
        TransactionService _transactionService;
        UserService _userService;
        /// <summary>
        /// Recursos disponibles para administradores del sistema.
        /// </summary>
        public AdminController(TransactionService transactionService, UserService userService)
        {
            _transactionService = transactionService;
            _userService = userService;
        }
        /// <summary>
        /// Reporte de las transacciones totales del sistema (diarias, mensuales, etc).
        /// </summary>
        [Route("reporte"), HttpGet]
        public HttpResponseMessage GetTransacciones()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _transactionService.ListarTransacciones());
        }
        /// <summary>
        /// Comparacion de balance total en dolares, de todas las wallets de dos usuarios.
        /// </summary>
        /// <param name="userName1">El nombre del primer usuario.</param>
        /// <param name="userName2">El nombre del segundo usuario.</param>
        [Route("compare"), HttpGet]
        public HttpResponseMessage GetComparacion([FromUri] string userName1, [FromUri] string userName2)
        {
            var user1 = _userService.GetUserByName(userName1);
            var user2 = _userService.GetUserByName(userName2);

            if (user1 == null || user2 == null) return Request.CreateResponse(HttpStatusCode.NotFound, "Usuario no encontrado");
            return Request.CreateResponse(HttpStatusCode.OK, _userService.GetUserComparisonResponse(user1, user2));
        }
        /// <summary>
        /// Obtiene la informacion administrativa de un usuario (Nombre, Id, Ultima fecha de acceso, cantidad de criptomonedas y transacciones).
        /// </summary>
        /// <param name="userId">El id del usuario</param>
        [Route("users/{userId}"), HttpGet]
        public HttpResponseMessage GetUser(int userId)
        {
            var user = _userService.GetUserById(userId);

            if (user == null) return Request.CreateResponse(HttpStatusCode.NotFound, "Usuario no encontrado");
            return Request.CreateResponse(HttpStatusCode.OK, _userService.GetUserAdminInfo(userId));
        }
        /// <summary>
        /// Obtiene la informacion administrativa de todos los usuarios (Nombre, Id, Ultima fecha de acceso, cantidad de criptomonedas y transacciones).
        /// </summary>
        [Route("users"), HttpGet]
        public HttpResponseMessage GetUsers()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _userService.GetUsers().Select(u => _userService.GetUserAdminInfo(u.Id)));
        }
        /// <summary>
        /// Crea un nuevo usuario con privilegios de admin.
        /// </summary>
        /// <param name="user">El nombre y password del nuevo administrador.</param>
        [Route(""), HttpPost]
        public HttpResponseMessage Post([FromBody]SignupRequest user)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Campos incorrectos");

            var usuario = _userService.SignUp(user.Username, user.Password, true);
            if (usuario != null)
            {
                return Request.CreateResponse(HttpStatusCode.Created, new UserViewModel(usuario)); 
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "El usuario ya existe");
            }
        }

    }
}