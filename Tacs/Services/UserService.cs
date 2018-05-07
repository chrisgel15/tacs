using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Tacs.Context;
using Tacs.Models;
using Tacs.Models.Contracts;
using Tacs.Models.Repositories;

namespace Tacs.Services
{
    public class UserService
    {
        public IList<User> GetUsers()
        {
            using (var unitOfWork = new UnitOfWork(new TacsDataContext()))
            {
                return unitOfWork.Users.GetAll().ToList();
            }
        }

        public AdminUserInfoResponse GetUserAdminInfo(int id)
        {
            using (var unitOfWork = new UnitOfWork(new TacsDataContext()))
            {
                User info = unitOfWork.Users.GetUserInfoById(id);
                if (info != null)
                {
                    // no muestro el campo password
                    return new AdminUserInfoResponse(info);
                }                
                return null;
            }
        }

        public UserViewModel GetUserInfo (int userId)
        {
            return new UserViewModel(GetUserById(userId));
        }

        public async Task<UserComparisonResponse> CompareUsers(int userId1, int userId2)
        {
            var context = new UnitOfWork(new TacsDataContext());

            User user1 = context.Users.GetUserInfoById(userId1);
            User user2 = context.Users.GetUserInfoById(userId2);

            return await new UserService().GetUserComparisonResponse(user1, user2);

        }

        public User GetUserByName(string userName)
        {
            var context = new UnitOfWork(new TacsDataContext());
            return context.Users.Find(u => u.Name.ToLower() == userName.ToLower()).First();
        }

        public User GetUserById(int userId)
        {
            var context = new UnitOfWork(new TacsDataContext());
            return context.Users.Get(userId);
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

        public async Task<UserComparisonResponse> GetUserComparisonResponse(User user1, User user2)
        {
            var response = new UserComparisonResponse();
            response.User1 = user1.Name;
            response.Patrimonio1 = await user1.GetPatrimonio();
            response.User2 = user2.Name;
            response.Patrimonio2 = await user2.GetPatrimonio();

            return response;
        }


    }
}