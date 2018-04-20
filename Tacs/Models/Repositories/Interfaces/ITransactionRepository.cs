using System.Collections.Generic;

namespace Tacs.Models.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        IEnumerable<Transaction> GetTopUsers(int count);
    }
}