using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tacs.Models.Exceptions;

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
        public DateTime LastAccessDate { get; set; }

        [DataMember]
        public virtual ICollection<UserCoin> UserCoins { get; set; }

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
            //this.Wallets = new List<Wallet>();
            this.UserCoins = new List<UserCoin>();
            this.LastAccessDate = DateTime.Now;
        }

        public User(string name, string password)
        {
            this.Name = name;
            this.Password = password;
            //   this.Wallets = new List<Wallet>();
            this.UserCoins = new List<UserCoin>();
            this.LastAccessDate = DateTime.Now;
        }

        public void Buy(Coin coin, int amount)
        {
            CreateUserCoins();
            UserCoin userCoins = GetUserCoins(coin);

            if (userCoins == null)
            {
                AddUserCoinToUser(coin, amount);
            }
            else
            {
                userCoins.Amount += amount;
            }
        }

        public void Sell(Coin coin, int amount)
        {
            CreateUserCoins();
            UserCoin userCoins = GetUserCoins(coin);

            if (userCoins == null)
            {
                throw new InvalidOperationException("You cant sell a Coin that you dont have.");
            }
            else
            {
                if (userCoins.Amount >= amount)
                    userCoins.Amount -= amount;
                else
                    throw new InsufficientAmountException("You are trying to sell more than you have.");
            }
        }

        private void AddUserCoinToUser(Coin coin, int amount)
        {
            UserCoin userCoin = new UserCoin()
            {
                Coin = coin,
                User = this,
                Amount = amount
            };

            this.UserCoins.Add(userCoin);
        }
        private UserCoin GetUserCoins(Coin coin)
        {
            return this.UserCoins.Where(uc => uc.CoinID == coin.Id).FirstOrDefault();
        }
        private void CreateUserCoins()
        {
            if (this.UserCoins == null)
                this.UserCoins = new List<UserCoin>();
        }

       
    }
}