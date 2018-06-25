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
        TransactionService _transactionService;
        UserService _userService;
        public UsersController(TransactionService transactionService, UserService userService)
        {
            _transactionService = transactionService;
            _userService = userService;
        }
        //Obtener todos los datos de un usuario. Solo lo tendria que poder usar el usuario logueado
        [Authorize, Route(""), HttpGet]
        public IHttpActionResult GetById()
        {
            // Desde el Identity, recupero el Id del usuario
            int userId = TokenService.GetIdClient(User.Identity as ClaimsIdentity);
            return Ok<UserViewModel>(_userService.GetUserInfo(userId));
        }

        [Authorize, Route("transactions"), HttpGet]
        public HttpResponseMessage GetTransactions()
        {
            // Desde el Identity, recupero el Id del usuario
            int userId = TokenService.GetIdClient(User.Identity as ClaimsIdentity);
            var transactions = _userService.GetUserById(userId).Transactions;
            var transactionsInfos = transactions.Select(t => _transactionService.GetTransactionInfo(t));
            return Request.CreateResponse<IList<TransactionViewModel>>(HttpStatusCode.OK, transactionsInfos.ToList());
        }

        //Agregar un nuevo usuario
        [AllowAnonymous, Route(""), HttpPost]
        public HttpResponseMessage Post([FromBody]SignupRequest user)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Campos incorrectos");

            bool estado = _userService.SignUp(user.Username, user.Password, false);
            if (estado)
            {
                return Request.CreateResponse<UserViewModel>(HttpStatusCode.Created, new UserViewModel(_userService.GetUserByName(user.Username)));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "El usuario ya existe");
            }
        }
    }
}