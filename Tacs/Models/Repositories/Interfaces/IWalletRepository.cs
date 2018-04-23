using System.Collections.Generic;

namespace Tacs.Models.Repositories
{
    public interface IWalletRepository : IRepository<Wallet>
    {
        IEnumerable<Wallet> GetTopUsers(int count);
        Wallet GetByUser(User user, Coin coin);
    }
}