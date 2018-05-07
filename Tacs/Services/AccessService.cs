using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tacs.Context;
using Tacs.Models;
using Tacs.Models.Repositories;

namespace Tacs.Services
{
    public class AccessService
    {
        public bool Login(User user)
        {
            using (var unitOfWork = new UnitOfWork(new TacsDataContext()))
            {
                var exists = unitOfWork.Users.ExistsUserByNameAndPassword(user);

                if (exists)
                { 
                    unitOfWork.Users.SetUserLastAccessDate(user);
                    unitOfWork.Complete();
                    return true;
                }

                return false;
            }

        }
    }
}