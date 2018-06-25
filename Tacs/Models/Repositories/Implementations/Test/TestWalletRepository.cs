using System.Collections.Generic;
using System.Linq;
using Tacs.Context;

namespace Tacs.Models.Repositories
{
    public class TestWalletRepository : TestRepository<Wallet>, IWalletRepository
    {
        public TestWalletRepository()
        {

        }
    }
}