using System;

namespace Tacs.Models.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; set; }
        ICoinRepository Coins { get; set; }
        IWalletRepository Wallets { get; set; }
        ITransactionRepository Transactions { get; set; }
        int Complete();
    }
}