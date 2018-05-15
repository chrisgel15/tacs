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
        [Route("reporte"), HttpGet]
        public HttpResponseMessage GetTransacciones()
        {
            return Request.CreateResponse(new TransactionService().ListarTransacciones());
        }

        //Comparacion de balance total en dolares, de todas las wallets de dos usuarios
        [Route("compare"), HttpGet]
        public HttpResponseMessage GetComparacion([FromUri] string userName1, [FromUri] string userName2)
        {
            var user1 = new UserService().GetUserByName(userName1);
            var user2 = new UserService().GetUserByName(userName2);

            return Request.CreateResponse(new UserService().GetUserComparisonResponse(user1, user2));
        }

        //Obtener todos los usuarios del sistema (hay que definir quien, si es que alguien, puede usar esto)
        [Route("users"), HttpGet]
        public IHttpActionResult Get()
        {
            return Ok<IList<UserViewModel>>(new UserService().GetUsers().Select(u => new UserService().GetUserInfo(u.Id)).ToList());
        }

        //Datos administrativos de un usuario
        [Route("users/{userId}"), HttpGet]
        public HttpResponseMessage GetUser(int userId)
        {
            return Request.CreateResponse(new UserService().GetUserAdminInfo(userId));
        }

        //Agregar un nuevo administrador
        [Route(""), HttpPost]
        public IHttpActionResult Post([FromBody]SignupRequest user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Campos incorrectos");

            bool estado = new UserService().SignUp(user.Username, user.Password, true);
            if (estado)
            {
                return Ok(); 
            }
            else
            {
                return BadRequest();
            }
        }

    }
}