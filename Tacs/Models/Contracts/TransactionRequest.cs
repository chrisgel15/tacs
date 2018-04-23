using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tacs.Models.Contracts
{
    [DataContract]
    public class TransactionRequest
    {
        [DataMember]
        [Required]
        public int UserId { get; set; }

        [DataMember]
        [Required]
        public int CoinId { get; set; }

        [DataMember]
        [Required]
        public int Amount { get; set; }

    }
}