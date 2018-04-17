using System.Collections.Generic;
using Tacs.Context;

namespace Tacs.Models.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(TacsDataContext context) : base(context)
        {
        }

        public IEnumerable<User> GetTopUsers(int count)
        {
            return TacsDataContext.Users;
        }

        public TacsDataContext TacsDataContext
        {
            get { return Context as TacsDataContext; }
        }
    }
}