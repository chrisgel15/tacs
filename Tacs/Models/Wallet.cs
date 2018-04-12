﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tacs.Models
{
    [DataContract]
    public class Wallet
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        [Required]
        public Coin Coin { get; set; }

        [DataMember]
        [Required]
        public int CoinId { get; set; }

        [DataMember]
        public int Amount { get; set; }

        [DataMember]
        [Required]
        public int UserId { get; set; }

        [DataMember]
        [Required]
        public User User { get; set; }

        public Wallet(User user, Coin coin)
        {
            User = user;
            Coin = coin;
            Amount = 0;
        }

        public void Buy(int amount)
        {
            this.Amount += amount;
        }

        public void Sell(int amount)
        {
            this.Amount -= amount;
        }

    }
}