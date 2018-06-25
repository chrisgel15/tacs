using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Tacs.Models;
using Tacs.Models.Contracts;
using Tacs.Services;

namespace Tacs.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SigninController : ApiController
    {
        AccessService _accessService;
        /// <summary>
        /// 
        /// </summary>
        public SigninController(AccessService accessService)
        {
            _accessService = accessService;
        }
        /// <summary>
        /// Obtiene un token, con las credenciales del usuario.
        /// </summary>
        /// <param name="request">El usuario y el Password del usuario.</param>
        public HttpResponseMessage Post([FromBody]SignInRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequesResponse();

            AccessService loginService = _accessService;

            if (loginService.Login(request.User))
            {
                return SuccessfullLoginResponse();
            }
            else
            {
                return NotFoundResponse();
            }
        }
        private HttpResponseMessage BadRequesResponse()
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Incorret input");
        }
        private HttpResponseMessage SuccessfullLoginResponse()
        {
            return Request.CreateResponse<SignInResponse>(HttpStatusCode.OK,
                new SignInResponse()
                {
                    LoggedIn = true,
                    Error = false,
                    Message = "Logged In Succesfully"
                }
                );
        }
        private HttpResponseMessage NotFoundResponse()
        {
            return Request.CreateResponse<SignInResponse>(HttpStatusCode.NotFound,
                new SignInResponse()
                {
                    LoggedIn = false,
                    Error = true,
                    Message = "Incorrect UserName or Password"
                });
        }
    }
}
