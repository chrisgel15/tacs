using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tacs.Services;
using Tacs.Models.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Tacs.Controllers
{
    [RoutePrefix("api/user")]
    public class UsersController : ApiController
    {
        //Obtener todos los datos de un usuario. Solo lo tendria que poder usar el usuario logueado
        [Authorize, Route(""), HttpGet]
        public IHttpActionResult GetById()
        {
            // Desde el Identity, recupero el Id del usuario
            int userId = TokenService.GetIdClient(User.Identity as ClaimsIdentity);
            return Ok<UserViewModel>(new UserService().GetUserInfo(userId));
        }

        //Agregar un nuevo usuario
        [AllowAnonymous, Route(""), HttpPost]
        public HttpResponseMessage Post([FromBody]SignupRequest user)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Campos incorrectos");

            bool estado = new UserService().SignUp(user.Username, user.Password, false);
            if (estado)
            {
                return Request.CreateResponse<UserViewModel>(HttpStatusCode.Created, new UserViewModel(new UserService().GetUserByName(user.Username)));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "El usuario ya existe");
            }
        }
    }
}