using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Tacs.Models;

namespace Tacs.Context
{
    public class TacsDataContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<TacsDataContext>(new TacsDataContextInitializer());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Coin> Coins { get; set; }
      //  public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<UserCoin> UserCoins { get; set; }
    }

    public class TacsDataContextInitializer : DropCreateDatabaseIfModelChanges<TacsDataContext>
    {
        protected override void Seed(TacsDataContext context)
        {
            base.Seed(context);
        }
    }
}