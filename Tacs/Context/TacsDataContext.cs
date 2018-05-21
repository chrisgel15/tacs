using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Tacs.Models;
using Tacs.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Tacs.Context
{
    public class TacsDataContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<TacsDataContext>(new MigrateDatabaseToLatestVersion<TacsDataContext, Configuration>());
            base.OnModelCreating(modelBuilder);
            base.Configuration.LazyLoadingEnabled = true;
            base.Configuration.ProxyCreationEnabled = true;
            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
            modelBuilder.Conventions.Add(new DecimalPropertyConvention(20, 10));
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Coin> Coins { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
    }
}