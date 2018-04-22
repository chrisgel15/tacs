using System.Collections.Generic;
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
            return TacsDataContext.Wallets;
        }

        
        public TacsDataContext TacsDataContext
        {
            get { return Context as TacsDataContext; }
        }
    }
}