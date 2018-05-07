using System.Collections.Generic;
using Tacs.Context;

namespace Tacs.Models.Repositories
{
    public class CoinRepository : Repository<Coin>, ICoinRepository
    {
        public CoinRepository(TacsDataContext context) : base(context)
        {
        }

        public IEnumerable<Coin> GetTopUsers(int count)
        {
            return TacsDataContext.Coins;
        }

        public TacsDataContext TacsDataContext
        {
            get { return Context as TacsDataContext; }
        }
    }
}