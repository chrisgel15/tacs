using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tacs.Context;
using Tacs.Models.Repositories;
using System.Security.Principal;
using System.Security.Claims;

namespace Tacs.Services
{
    public class TokenService
    {
        public static int GetIdClient(ClaimsIdentity identity)
        {
            Claim p = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return Int32.Parse(p.Value);
        }

    }
}