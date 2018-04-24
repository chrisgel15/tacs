using System.Collections.Generic;
using System.Linq;
using Tacs.Context;

namespace Tacs.Models.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(TacsDataContext context) : base(context)
        {

        }

        public TacsDataContext TacsDataContext
        {
            get { return Context as TacsDataContext; }
        }

        public IList<Transaction> GetByCoinId(int coinId)
        {
            var compras = TacsDataContext.Transactions.Include("User").OfType<Compra>().Where(t => t.Coin.Id == coinId).ToList();
            return TacsDataContext.Transactions.Include("User").Where(t => t.Coin.Id == coinId).ToList();
        }
    }
}