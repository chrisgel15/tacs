using System.Collections.Generic;
using Tacs.Context;

namespace Tacs.Models.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(TacsDataContext context) : base(context)
        {
        }

        public IEnumerable<Transaction> GetTopUsers(int count)
        {
            return TacsDataContext.Transactions;
        }

        public TacsDataContext TacsDataContext
        {
            get { return Context as TacsDataContext; }
        }
    }
}