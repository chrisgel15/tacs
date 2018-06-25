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
    public class SigninController : ApiController
    {
        AccessService _accessService;
        SigninController(AccessService accessService)
        {
            _accessService = accessService;
        }
        // POST api/signin
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
