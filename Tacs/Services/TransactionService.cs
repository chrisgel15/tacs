﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tacs.Context;
using Tacs.Models;
using Tacs.Models.Repositories;
using Tacs.Models.Contracts;

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

        public IList<Transaction> GetTransactionsByCoinId(int coinId)
        {
            using (var unitOfWork = new UnitOfWork(new TacsDataContext()))
            {
                IList<Transaction> transactions = unitOfWork.Transactions.GetByCoinId(coinId);

                unitOfWork.Complete();

                return transactions;
            }

        }


        public AdminTransactionsResponse ListarTransacciones()
        {
            AdminTransactionsResponse response = new AdminTransactionsResponse();
            var context = new UnitOfWork(new TacsDataContext());

            var date = DateTime.Today.AddDays(-1);
            response.TransaccionesHoy = context.Transactions.Find(t => t.Date > date).Count();
            date = DateTime.Today.AddDays(-3);
            response.TransaccionesUltimosTresDias = context.Transactions.Find(t => t.Date > date).Count();
            date = DateTime.Today.AddDays(-7);
            response.TransaccionesUltimaSemana = context.Transactions.Find(t => t.Date > date).Count();
            date = DateTime.Today.AddMonths(-1);
            response.TransaccionesUltimoMes = context.Transactions.Find(t => t.Date > date).Count();
            response.TransaccionesTotales = context.Transactions.GetAll().Count();

            return response;
        }
    }
}