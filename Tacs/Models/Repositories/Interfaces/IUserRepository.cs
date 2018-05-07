using System.Collections.Generic;

namespace Tacs.Models.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        bool ExistsUserByNameAndPassword(User user);

        void SetUserLastAccessDate(User user);

        User GetUserInfoById(int id);

        bool ExistUserByName(string name);

        void AddNewUser(User user);

        int GetMaxId();

        User GetUserWithCoins(int userId);
    }
}