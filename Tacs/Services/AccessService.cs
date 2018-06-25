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
        IUnitOfWork _unitOfWork;
        public AccessService()
        {
        }
        public AccessService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool Login(User user)
        {
   
                var exists = _unitOfWork.Users.ExistsUserByNameAndPassword(user);

                if (exists)
                { 
                    _unitOfWork.Users.SetUserLastAccessDate(user);
                    _unitOfWork.Complete();
                    return true;
                }

                return false;
            

        }
    }
}