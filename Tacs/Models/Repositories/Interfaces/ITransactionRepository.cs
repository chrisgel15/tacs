using System.Collections.Generic;

namespace Tacs.Models.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        IList<Transaction> GetByCoinId(int coinId);
    }
}