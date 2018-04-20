using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tacs.Models
{
    [DataContract]
    public abstract class Transaction
    {
        public Transaction(User user, Coin coin, int amount, DateTime date, decimal price)
        {
            this.User = user;
            this.Coin = coin;
            this.Amount = amount;
            this.Date = date;
            this.Price = price;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public Coin Coin { get; set; }

        [DataMember]
        [Required]
        public User User { get; set; }

        [DataMember]
        [Required]
        public int Amount { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public decimal Price { get; set; }
    }
}