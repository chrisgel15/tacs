using System.Collections.Generic;
using System.Runtime.Serialization;
using Tacs.Services;

namespace Tacs.Models
{
    [DataContract]
    public class Coin
    {
        // For Entity Framework Code First Needs...
        // Check: https://stackoverflow.com/questions/31543255/why-must-i-have-a-parameterless-constructor-for-code-first-entity-framework
        private Coin()
        {

        }
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public virtual List<UserCoin> UserCoins { get; set; }
        public virtual List<Transaction> Transactions { get; set; }

        public Coin(string name)
        {
            this.Name = name;
        }

        public Coin(string name, int Id)
        {
            this.Name = name;
            this.Id = Id;
        }

        public decimal GetCotizacion()
        {
            return CotizacionService.GetCotizacion(this.Name);
        }
    }
}