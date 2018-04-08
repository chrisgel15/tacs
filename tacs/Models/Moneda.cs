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
        public string nombre { get; set; }

        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string cotizacion { get; set; }
    }
}