using System.Collections.Generic;

namespace Tacs.Models.Repositories
{
    public interface ICoinRepository : IRepository<Coin>
    {
        IEnumerable<Coin> GetTopUsers(int count);
    }
}