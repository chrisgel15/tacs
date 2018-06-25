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

        public TacsDataContext TacsDataContext
        {
            get { return Context as TacsDataContext; }
        }

        public Wallet GetByUserAndCoin(User user, Coin coin)
        {
            return user.Wallets.FirstOrDefault(uc => uc.Coin == coin);
        }
    }
}