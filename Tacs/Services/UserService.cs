using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tacs.Context;
using Tacs.Models;
using Tacs.Models.Contracts;
using Tacs.Models.Repositories;

namespace Tacs.Services
{
    public class UserService
    {
        public IList<UserInfoResponse> GetUsers()
        {
            using (var unitOfWork = new UnitOfWork(new TacsDataContext()))
            {
                IList<User> users = unitOfWork.Users.GetAll().ToList();
                if (users.Count() > 0)
                {
                    var usersInfo = from user in users select new UserInfoResponse(user);
                    return usersInfo.ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        public UserInfoResponse GetInfo(int id)
        {
            using (var unitOfWork = new UnitOfWork(new TacsDataContext()))
            {
                User info = unitOfWork.Users.GetUserInfoById(id);
                if (info != null)
                {
                    // no muestro el campo password
                    return new UserInfoResponse(info);
                }                
                return null;
            }
        }

        public UserComparisonResponse CompareUsers(int userId1, int userId2)
        {
            var context = new UnitOfWork(new TacsDataContext());

            User user1 = context.Users.GetUserInfoById(userId1);
            User user2 = context.Users.GetUserInfoById(userId2);

            return new UserComparisonResponse(user1, user2);

        }

        public dynamic SignUp(string username, string password)
        {
            using (var unitOfWork = new UnitOfWork(new TacsDataContext()))
            {
                var dbUsers = unitOfWork.Users;
                if (dbUsers.ExistUserByName(username))
                {
                    return new { estado = "ERROR", codigo = "USERNAME_DUPLICATE", descripcion = "Ya existe ese Username en la base" };
                }
                else
                {
                    int id = dbUsers.GetMaxId() + 1;
                    dbUsers.AddNewUser(new User(id, username, password));
                    unitOfWork.Complete();
                    return new { estado = "OK", descripcion = "user created (id: "+id.ToString()+")", date = DateTime.Now.ToString() };
                }
            }
        }


    }
}