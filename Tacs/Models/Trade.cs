using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tacs.Models
{
    [DataContract]
    public class Trade
    {
        [DataMember]
        public string CoinId { get; set; }

        [DataMember]
        public UInt32 Amount { get; set; }
    }
}