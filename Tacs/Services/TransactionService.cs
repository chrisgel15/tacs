using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tacs.Context;
using Tacs.Models;
using Tacs.Models.Repositories;

namespace Tacs.Services
{
    public class TransactionService
    {
        public void Buy(int userId, int coinId, int amount)
        {
            using (var unitOfWork = new UnitOfWork(new TacsDataContext()))
            {
                User user;
                Coin coin;
                GetUserAndCoin(userId, coinId, unitOfWork, out user, out coin);

                user.Buy(coin, amount);

                unitOfWork.Transactions.Add(new Compra(user, coin, amount, DateTime.Now, new decimal(1.00)));

                unitOfWork.Complete();
            }
        }

        private void GetUserAndCoin(int userId, int coinId, UnitOfWork unitOfWork, out User user, out Coin coin)
        {
            user = unitOfWork.Users.GetUserWithCoins(userId);
            if (user == null)
                throw new BusinnesException("El usuario no existe.");

            coin = unitOfWork.Coins.Get(coinId);

            // TODO: Check how to handle this scenario, because we should add the coin by the name maybe?
            if (coin == null)
            {
                coin = new Coin(coinId.ToString());
                unitOfWork.Coins.Add(coin);
            }
        }

        public void Sale(int userId, int coinId, int amount)
        {
            using (var unitOfWork = new UnitOfWork(new TacsDataContext()))
            {
                User user;
                Coin coin;
                GetUserAndCoin(userId, coinId, unitOfWork, out user, out coin);

                user.Sell(coin, amount);

                unitOfWork.Transactions.Add(new Venta(user, coin, amount, DateTime.Now, new decimal(1.00)));

                unitOfWork.Complete();
            }
        }
    }
}