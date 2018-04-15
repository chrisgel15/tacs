namespace Tacs.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using Tacs.Models.Exceptions;

    /// <summary>
    /// Wallet Class
    /// </summary>
    [DataContract]
    public class Wallet
    {
        public Wallet(User user, Coin coin)
        {
            User = user;
            Coin = coin;
            this.Amount = 0;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        [Required]
        public Coin Coin { get; set; }

        [DataMember]
        public int Amount { get; set; }

        [DataMember]
        [Required]
        public User User { get; set; }

        #region For Entity Framework Navigation
        [DataMember]
        [Required]
        public int CoinId { get; set; }

        [DataMember]
        [Required]
        public int UserId { get; set; }

#endregion
     
        public void Buy(int amount)
        {
            this.Amount += amount;
        }

        public void Sell(int amount)
        {
            if (this.Amount >= amount)
            {
                this.Amount -= amount;
            }
            else
            {
                throw new InsufficientAmountException("You are trying to sell more than you have.");
            }
        }
    }
}