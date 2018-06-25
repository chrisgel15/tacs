using System.Collections.Generic;
using Tacs.Context;

namespace Tacs.Models.Repositories
{
    public class TestUnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; set; }
        public ICoinRepository Coins { get; set; }
        public IWalletRepository Wallets { get; set; }
        public ITransactionRepository Transactions { get; set; }
        public TestUnitOfWork()
        {
            Users = new TestUserRepository();
            Coins = new TestCoinRepository();
            Wallets = new TestWalletRepository();
            Transactions = new TestTransactionRepository();
        }

        public void Dispose()
        {
            Users = new TestUserRepository();
            Coins = new TestCoinRepository();
            Wallets = new TestWalletRepository();
            Transactions = new TestTransactionRepository();
        }
        public int Complete()
        {
            return 0;
        }
    }
}