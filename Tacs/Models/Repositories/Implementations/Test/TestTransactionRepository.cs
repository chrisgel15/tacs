using System.Collections.Generic;
using System.Linq;
using Tacs.Context;

namespace Tacs.Models.Repositories
{
    public class TestTransactionRepository : TestRepository<Transaction>, ITransactionRepository
    {
        public TestTransactionRepository()
        {

        }
    }
}