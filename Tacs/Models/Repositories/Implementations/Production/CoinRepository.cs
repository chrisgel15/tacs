using System.Collections.Generic;
using Tacs.Context;

namespace Tacs.Models.Repositories
{
    public class CoinRepository : Repository<Coin>, ICoinRepository
    {
        public CoinRepository(TacsDataContext context) : base(context)
        {
        }
    }
}