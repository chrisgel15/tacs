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

        public bool SaveToken(string username, string token)
        {
            var unitOfWork = new UnitOfWork(new TacsDataContext());
            var user = unitOfWork.Users.Find(u => u.Name == username).FirstOrDefault();

            if (user != null)
            {
                user.Token = token;
                user.LastAccessDate = DateTime.Now;
                unitOfWork.Complete();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveToken(string token)
        {
            var unitOfWork = new UnitOfWork(new TacsDataContext());
            var user = unitOfWork.Users.Find(u => u.Token == token).FirstOrDefault();

            if (user != null)
            {
                user.Token = "";
                user.LastAccessDate = DateTime.Now;
                unitOfWork.Complete();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}