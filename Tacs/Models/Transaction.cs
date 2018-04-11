using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tacs.Models
{
    [DataContract]
    public class Transaction
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Coin CoinId { get; set; }

        [DataMember]
        public User UserId { get; set; }

        [DataMember]
        public int Amount { get; set; }

        [DataMember]
        public DateTime Date { get; set; }
    }
}