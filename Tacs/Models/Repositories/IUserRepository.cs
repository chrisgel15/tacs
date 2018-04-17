using System.Collections.Generic;

namespace Tacs.Models.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetTopUsers(int count);
    }
}