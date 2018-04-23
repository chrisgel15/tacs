using System.Collections.Generic;

namespace Tacs.Models.Repositories
{
    public interface IUserCoinRepository : IRepository<UserCoin>
    {
        //IEnumerable<Coin> GetTopUsers(int count);
        UserCoin GetByUserAndCoin(User user, Coin coin);
    }
}