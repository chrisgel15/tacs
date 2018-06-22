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
        protected Transaction()
        {

        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public virtual Coin Coin { get; set; }

        [DataMember]
        [Required]
        public virtual User User { get; set; }
        [DataMember]
        [Required]
        public virtual Wallet Wallet { get; set; }

        [DataMember]
        [Required]
        public decimal Amount { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        abstract public string Type();
    }

    public class Venta : Transaction
    {
        protected Venta()
        {

        }
        public Venta(Wallet wallet, decimal amount)
        {
            this.Price = wallet.Coin.GetCotizacion().Result;
            this.Coin = wallet.Coin;
            this.User = wallet.User;
            this.Date = DateTime.Now;
            this.Wallet = wallet;
            this.Amount = amount;
        }

        override public string Type()
        {
            return "venta";
        }

    }

    public class Compra : Transaction
    {
        protected Compra()
        {

        }
        public Compra(Wallet wallet, decimal amount)
        {
            this.Price = wallet.Coin.GetCotizacion().Result;
            this.Coin = wallet.Coin;
            this.User = wallet.User;
            this.Date = DateTime.Now;
            this.Wallet = wallet;
            this.Amount = amount;
        }

        override public string Type()
        {
            return "compra";
        }
    }
}