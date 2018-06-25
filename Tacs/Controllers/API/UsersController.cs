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
    /// <summary>
    /// Permite registrar nuevos usuarios y da acceso a las operaciones regulares al usuario autenticado.
    /// </summary>
    [RoutePrefix("api/user")]
    public class UsersController : ApiController
    {
        TransactionService _transactionService;
        UserService _userService;
        /// <summary>
        /// Permite registrar nuevos usuarios y da acceso a las operaciones regulares al usuario autenticado.
        /// </summary>
        public UsersController(TransactionService transactionService, UserService userService)
        {
            _transactionService = transactionService;
            _userService = userService;
        }
        /// <summary>
        /// Devuelve los datos del usuario.
        /// </summary>
        [Authorize, Route(""), HttpGet]
        public IHttpActionResult GetUser()
        {
            // Desde el Identity, recupero el Id del usuario
            int userId = TokenService.GetIdClient(User.Identity as ClaimsIdentity);
            return Ok<UserViewModel>(_userService.GetUserInfo(userId));
        }
        /// <summary>
        /// Devuelve todas las transacciones del usuario.
        /// </summary>
        [Authorize, Route("transactions"), HttpGet]
        public HttpResponseMessage GetTransactions()
        {
            // Desde el Identity, recupero el Id del usuario
            int userId = TokenService.GetIdClient(User.Identity as ClaimsIdentity);
            var transactions = _userService.GetUserById(userId).Transactions;
            var transactionsInfos = transactions.Select(t => _transactionService.GetTransactionInfo(t));
            return Request.CreateResponse<IList<TransactionViewModel>>(HttpStatusCode.OK, transactionsInfos.ToList());
        }
        /// <summary>
        /// Agrega un nuevo usuario regular (sin privilegios de admin).
        /// </summary>
        /// <param name="user">El nombre y la contraseña del nuevo usuario</param>
        [AllowAnonymous, Route(""), HttpPost]
        public HttpResponseMessage Post([FromBody]SignupRequest user)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Campos incorrectos");

            var usuario = _userService.SignUp(user.Username, user.Password, false);
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