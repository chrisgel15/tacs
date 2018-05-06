using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web;
using Tacs.Models.Exceptions;

namespace Tacs.Models
{
    [DataContract]
    public class User
    {
        public int Id { get; set; }
        [Index(IsUnique = true)]
        [StringLength(450)]
        [Required]
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime LastAccessDate { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public string Token { get; set; }
        public string EsAdmin { get; set; }


        protected User()
        {
            Wallets = new List<Wallet>();
            Transactions = new List<Transaction>();
        }

        public User(int id, string name, string password)
        {
            this.Id = id;
            this.Name = name;
            this.Password = password;
            this.LastAccessDate = DateTime.Now;
            Wallets = new List<Wallet>();
            Transactions = new List<Transaction>();
        }


        public User(int id, string name, string password, string esAdminRole)
        {
            this.Id = id;
            this.Name = name;
            this.Password = password;
            this.LastAccessDate = DateTime.Now;
            Wallets = new List<Wallet>();
            Transactions = new List<Transaction>();
            this.EsAdmin = esAdminRole;
        }

        public User(string name, string password)
        {
            this.Name = name;
            this.Password = password;
            this.Transactions = new List<Transaction>();
            this.Wallets = new List<Wallet>();
            this.LastAccessDate = DateTime.Now;
            Wallets = new List<Wallet>();
            Transactions = new List<Transaction>();
        }

        public async Task<decimal> GetPatrimonio()
        {
            var TasksDeBalancesEnDolares = this.Wallets.Select(async w => await w.Coin.GetCotizacion() * w.Balance);
            var balancesEnDolares = await Task.WhenAll(TasksDeBalancesEnDolares);
            return balancesEnDolares.Sum();
        }

        public void Buy(Coin coin, decimal amount)
        {
            CreateWallets();
            Wallet Wallets = GetWallets(coin);

            if (Wallets == null)
            {
                AddWalletToUser(coin, amount);
            }
            else
            {
                Wallets.Balance += amount;
            }
        }

        public void Sell(Coin coin, decimal amount)
        {
            CreateWallets();
            Wallet Wallets = GetWallets(coin);

            if (Wallets == null)
            {
                throw new InvalidOperationException("You cant sell a Coin that you dont have.");
            }
            else
            {
                if (Wallets.Balance >= amount)
                    Wallets.Balance -= amount;
                else
                    throw new InsufficientAmountException("You are trying to sell more than you have.");
            }
        }


        private void AddWalletToUser(Coin coin, decimal amount)
        {
            this.Wallets.Add(new Wallet(this, coin, amount));
        }
        private Wallet GetWallets(Coin coin)
        {
            return this.Wallets.Where(uc => uc.Coin.Id == coin.Id).FirstOrDefault();
        }
        private void CreateWallets()
        {
            if (this.Wallets == null)
                this.Wallets = new List<Wallet>();
        }

        
       
    }
}