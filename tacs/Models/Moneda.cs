using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tacs.Models
{
    [DataContract]
    public class Moneda
    {
        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string symbol { get; set; }
        [DataMember]
        public string rank { get; set; }
        [DataMember]
        public string price_usd { get; set; }
        [DataMember]
        public string price_btc { get; set; }
        [DataMember]
        public string _24h_volume_usd { get; set; }
        [DataMember]
        public string market_cap_usd { get; set; }
        [DataMember]
        public string available_supply { get; set; }
        [DataMember]
        public string percent_change_1h { get; set; }
        [DataMember]
        public string percent_change_24h { get; set; }
        [DataMember]
        public string perc_ent_change_7d { get; set; }
        [DataMember]
        public string last_updated { get; set; }



    }
}