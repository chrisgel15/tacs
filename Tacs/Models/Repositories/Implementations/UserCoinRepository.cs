using System.Collections.Generic;
using System.Linq;
using Tacs.Context;

namespace Tacs.Models.Repositories
{
    public class UserCoinRepository : Repository<UserCoin>, IUserCoinRepository
    {
        public UserCoinRepository(TacsDataContext context) : base(context)
        {

        }

        public TacsDataContext TacsDataContext
        {
            get { return Context as TacsDataContext; }
        }

        public UserCoin GetByUserAndCoin(User user, Coin coin)
        {
            return Find(uc => uc.CoinID == coin.Id && uc.UserID == coin.Id).FirstOrDefault();
        }
    }
}