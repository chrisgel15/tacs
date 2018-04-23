using System;
using System.Collections.Generic;
using System.Linq;
using Tacs.Context;

namespace Tacs.Models.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(TacsDataContext context) : base(context)
        {

        }

        public void SetUserLastAccessDate(User user)
        {
            user.LastAccessDate = DateTime.Now;
        }

        public bool ExistsUserByNameAndPassword(User user)
        {
           var existUser = Find(u => (u.Name == user.Name) && (u.Password == user.Password)).ToList();

           return existUser.Count > 0;
        }

        public User GetUserWithCoins(int userId)
        {
            return TacsDataContext.Users.Include("UserCoins").Where(o => o.Id == userId).FirstOrDefault();
        }

        public TacsDataContext TacsDataContext
        {
            get { return Context as TacsDataContext; }
        }
    }
}