using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tacs.Models
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public virtual IList<Wallet> Wallets { get; set; }

        [DataMember]
        public DateTime LastAccessDate { get; set; }

        // For Entity Framework Code First Needs...
        // Check: https://stackoverflow.com/questions/31543255/why-must-i-have-a-parameterless-constructor-for-code-first-entity-framework
        private User()
        {

        }

        public User(int id, string name, string password)
        {
            this.Id = id;
            this.Name = name;
            this.Password = password;
            this.Wallets = new List<Wallet>();
            this.LastAccessDate = DateTime.Now;
        }

        public User(string name, string password)
        {
            this.Name = name;
            this.Password = password;
            this.Wallets = new List<Wallet>();
            this.LastAccessDate = DateTime.Now;
        }

        public void Buy(Coin coin, int amount)
        {
            Wallet wallet = GetWallet(coin);

            wallet.Buy(amount);
        }

        public void Sell(Coin coin, int amount)
        {
            Wallet wallet = GetWallet(coin);

            wallet.Sell(amount);
        }

        private Wallet GetWallet(Coin coin)
        {
            Wallet wallet = this.Wallets.FirstOrDefault(c => c.Coin.Id == coin.Id);

            if (wallet == null)
            {
                wallet = new Wallet(this, coin);
                Wallets.Add(wallet);
            }

            return wallet;
        }
    }
}