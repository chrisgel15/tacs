using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tacs.Models.Contracts
{
    [DataContract]
    public class GetTransactionsByCoinRequest
    {      
        [DataMember]
        [Required]
        public int CoinId { get; set; }     
    }
}