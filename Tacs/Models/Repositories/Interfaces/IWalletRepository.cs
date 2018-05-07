using System.Collections.Generic;

namespace Tacs.Models.Repositories
{
    public interface IWalletRepository : IRepository<Wallet>
    {
        //IEnumerable<Coin> GetTopUsers(int count);
        Wallet GetByUserAndCoin(User user, Coin coin);
    }
}