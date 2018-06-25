using Tacs.Context;

namespace Tacs.Models.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TacsDataContext _context;
        public static TacsDataContext sessionContext;
        public IUserRepository Users { get; set; }
        public ICoinRepository Coins { get; set; }
        public IWalletRepository Wallets { get; set; }

        public ITransactionRepository Transactions { get; set; }

        public UnitOfWork(TacsDataContext context)
        {
            if (UnitOfWork.sessionContext == null) UnitOfWork.sessionContext = context;
            _context = UnitOfWork.sessionContext;
            Users = new UserRepository(sessionContext);
            Coins = new CoinRepository(sessionContext);
            Wallets = new WalletRepository(sessionContext);
            Transactions = new TransactionRepository(sessionContext);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}