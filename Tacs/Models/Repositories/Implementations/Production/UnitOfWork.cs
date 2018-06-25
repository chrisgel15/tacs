using Tacs.Context;

namespace Tacs.Models.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TacsDataContext _context;
        public IUserRepository Users { get; set; }
        public ICoinRepository Coins { get; set; }
        public IWalletRepository Wallets { get; set; }

        public ITransactionRepository Transactions { get; set; }

        public UnitOfWork(TacsDataContext context)
        {
            _context = context;
            Users = new UserRepository(context);
            Coins = new CoinRepository(context);
            Wallets = new WalletRepository(context);
            Transactions = new TransactionRepository(context);
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