using System.Collections.Generic;

namespace Tacs.Models.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        bool ExistsUserByNameAndPassword(User user);

        void SetUserLastAccessDate(User user);

        User GetUserWithCoins(int userId);
    }
}