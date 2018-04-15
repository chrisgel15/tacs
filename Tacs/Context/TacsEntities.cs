using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Tacs.Models;

namespace Tacs.Context
{
    public class TacsEntities : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Coin> Coins { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Compra> Compras { get; set; }

        public DbSet<Venta> Ventas { get; set; }
    }
}