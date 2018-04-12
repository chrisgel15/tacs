using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}