using System;
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
        public Transaction Buy(int walletId, decimal amount)
        {
            using (var unitOfWork = new UnitOfWork(new TacsDataContext()))
            {
                var wallet = unitOfWork.Wallets.Get(walletId);

                wallet.User.Buy(wallet.Coin, amount);
                
                unitOfWork.Transactions.Add(new Compra(wallet, amount));

                unitOfWork.Complete();

                return wallet.Transactions.OrderBy(t => t.Date).Last();
            }
        }

        public Transaction Sell(int walletId, decimal amount)
        {
            using (var unitOfWork = new UnitOfWork(new TacsDataContext()))
            {
                var wallet = unitOfWork.Wallets.Get(walletId);

                wallet.User.Sell(wallet.Coin, amount);

                unitOfWork.Transactions.Add(new Venta(wallet, amount));

                unitOfWork.Complete();

                return wallet.Transactions.OrderBy(t => t.Date).Last();
            }
        }

        public TransactionViewModel GetTransactionInfo(int transactionId)
        {
            Transaction transaction = new UnitOfWork(new TacsDataContext()).Transactions.Get(transactionId);
            return GetTransactionInfo(transaction);
        }
        public TransactionViewModel GetTransactionInfo(Transaction transaction)
        {
            return new TransactionViewModel(transaction);
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