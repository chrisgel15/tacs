using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Tacs.Services;

namespace Tacs.Models
{
    [DataContract]
    public class Coin
    {
        protected Coin()
        {
            Transactions = new List<Transaction>();
            Wallets = new List<Wallet>();
        }
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Index(IsUnique = true)]
        [StringLength(450)]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public virtual ICollection<Wallet> Wallets { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        public Coin(string name)
        {
            Transactions = new List<Transaction>();
            Wallets = new List<Wallet>();
            this.Name = name;
        }

        public Coin(string name, int Id)
        {
            Transactions = new List<Transaction>();
            Wallets = new List<Wallet>();
            this.Name = name;
            this.Id = Id;
        }

        public async Task<decimal> GetCotizacion()
        {
            return await CoinService.GetCotizacion(this.Name);
        }
    }
}