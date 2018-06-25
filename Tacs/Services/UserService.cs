using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Tacs.Context;
using Tacs.Models;
using Tacs.Models.Contracts;
using Tacs.Models.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace Tacs.Services
{
    public class UserService
    {
        public IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IList<User> GetUsers()
        {
           return _unitOfWork.Users.GetAll().ToList();
        }

        public User GetUserByNameAndPass(string username, string password)
        {
            return _unitOfWork.Users.Find(u => u.Name.ToLower() == username.ToLower() && u.Password == password).First();
        }

        public AdminUserInfoResponse GetUserAdminInfo(int id)
        {
                User info = _unitOfWork.Users.GetUserInfoById(id);
                if (info != null)
                {
                    // no muestro el campo password
                    return new AdminUserInfoResponse(info);
                }                
                return null;
        }

        public UserViewModel GetUserInfo (int userId)
        {
            return new UserViewModel(GetUserById(userId));
        }

        public async Task<UserComparisonResponse> CompareUsers(int userId1, int userId2)
        {
            User user1 = _unitOfWork.Users.GetUserInfoById(userId1);
            User user2 = _unitOfWork.Users.GetUserInfoById(userId2);

            return await GetUserComparisonResponse(user1, user2);
        }

        public User GetUserByName(string userName)
        {
            return _unitOfWork.Users.Find(u => u.Name.ToLower() == userName.ToLower()).FirstOrDefault();
        }

        public User GetUserById(int userId)
        {
            return _unitOfWork.Users.Get(userId);
        }

        public User SignUp(string username, string password, bool EsAdmin)
        {
                var dbUsers = _unitOfWork.Users;

                if (dbUsers.ExistUserByName(username))
                {
                    return null;
                }

                int id = dbUsers.GetMaxId() + 1;
                SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();
                string hashedPassword = Encoding.Default.GetString(provider.ComputeHash(Encoding.UTF8.GetBytes(password)));

                if (dbUsers.GetAll().Where(u => u.EsAdmin == "SI").Count() == 0) // si no hay admins, sera administrador (si se crea por /api/admin).
                {
                    dbUsers.AddNewUser(new User(id, username, hashedPassword, "SI"));
                }
                else
                {
                    dbUsers.AddNewUser(new User(id, username, hashedPassword, EsAdmin ? "SI" : "NO"));
                }

                _unitOfWork.Complete();


                return _unitOfWork.Users.Find(u => u.Name == username).FirstOrDefault();
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