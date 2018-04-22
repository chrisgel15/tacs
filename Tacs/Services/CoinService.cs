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
        public IList<Wallet> VerPortfolio(int userid)
        {
            using (var unitOfWork = new UnitOfWork(new TacsDataContext()))
            {
                var wallets = unitOfWork.Wallets.GetAll().Where(w => w.User.Id == userid).ToList();
                
                return wallets;
            }
        }
    }
}