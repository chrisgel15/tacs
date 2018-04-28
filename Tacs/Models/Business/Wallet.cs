using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Tacs.Models.Exceptions;

namespace Tacs.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Coin Coin { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public decimal Balance { get; set; }

        protected Wallet()
        {
            Transactions = new List<Transaction>();
        }

        public Wallet(User usuario, Coin moneda, decimal balanceInicial)
        {
            Transactions = new List<Transaction>();
            this.User = usuario;
            this.Coin = moneda;
            this.Balance = balanceInicial;
        }

        public void Buy(int amount)
        {
            this.Balance += amount;
        }

        public void Sell(int amount)
        {
            if (this.Balance >= amount)
            {
                this.Balance -= amount;
            }
            else
            {
                throw new InsufficientAmountException("You are trying to sell more than you have.");
            }
        }
    }
}