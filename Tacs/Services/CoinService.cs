using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tacs.Models;
using Tacs.Models.Repositories;
using Tacs.Context;


namespace Tacs.Services
{
    public class CoinService
    {
        public IList<UserCoin> VerPortfolio(int userid)
        {
            using (var unitOfWork = new UnitOfWork(new TacsDataContext()))
            {
                var coins = unitOfWork.UserCoins.GetAll().Where(uc => uc.User.Id == userid).ToList();
                
                return coins;
            }
        }
    }
}