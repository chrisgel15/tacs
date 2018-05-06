using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tacs.Context;
using Tacs.Models.Repositories;

namespace Tacs.Services
{
    public class TokenService
    {
        public bool SaveToken(string username, string token)
        {
            var unitOfWork = new UnitOfWork(new TacsDataContext());
            var user = unitOfWork.Users.Find(u => u.Name == username).FirstOrDefault();

            if (user != null)
            {
                user.Token = token;
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