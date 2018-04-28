using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tacs.Services;
using Tacs.Models.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Tacs.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        //Obtener todos los usuarios del sistema (hay que definir quien, si es que alguien, puede usar esto)
        [Route(""), HttpGet]
        public IHttpActionResult Get()
        {
            return Ok<IList<UserViewModel>>(new UserService().GetUsers().Select(u => new UserService().GetUserInfo(u.Id)).ToList());
        }

        //Obtener todos los datos de un usuario. Solo lo tendria que poder usar el usuario {userId}
        [Route("{userid}"), HttpGet]
        public IHttpActionResult GetById(int userId)
        {
            return Ok<UserViewModel>(new UserService().GetUserInfo(userId));
        }

        //Agregar un nuevo usuario. Solo tendria que poder usarlo un admin
        [Route(""), HttpPost]
        public IHttpActionResult Post([FromBody]SignupRequest user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Campos incorrectos");

            var responseService = new UserService().SignUp(user.Username, user.Password);
            if (responseService.estado == "ERROR")
            {
                return Ok(new { responseService });
            }
            //var response = Request.CreateResponse(HttpStatusCode.Created);
            return Created("", responseService);
        }
    }
}