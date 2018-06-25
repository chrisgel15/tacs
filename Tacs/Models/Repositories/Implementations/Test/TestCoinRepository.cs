using System.Collections.Generic;
using Tacs.Context;

namespace Tacs.Models.Repositories
{
    public class TestCoinRepository : TestRepository<Coin>, ICoinRepository
    {
        public TestCoinRepository()
        {
        }
    }
}