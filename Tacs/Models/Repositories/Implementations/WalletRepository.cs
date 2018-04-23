using System.Collections.Generic;
using System.Linq;
using Tacs.Context;

namespace Tacs.Models.Repositories
{
    public class WalletRepository : Repository<Wallet>, IWalletRepository
    {
        public WalletRepository(TacsDataContext context) : base(context)
        {
        }

        public IEnumerable<Wallet> GetTopUsers(int count)
        {
            return null;
         //   return TacsDataContext.Wallets;
        }

        public Wallet GetByUserAndCoin(int userId, int coinId)
        {
            return null;
           // return Find(w => w.UserId == user.Id && w.CoinId == coin.Id).FirstOrDefault();
        }

        public Wallet GetByUser(User user, Coin coin)
        {
            throw new System.NotImplementedException();
        }

        public TacsDataContext TacsDataContext
        {
            get { return Context as TacsDataContext; }
        }
    }
}