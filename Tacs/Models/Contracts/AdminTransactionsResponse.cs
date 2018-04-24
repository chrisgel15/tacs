using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tacs.Models.Contracts
{
    [DataContract]
    public class AdminTransactionsResponse
    {
        [DataMember]
        public int TransaccionesHoy { get; set; }

        [DataMember]
        public int TransaccionesUltimosTresDias { get; set; }

        [DataMember]
        public int TransaccionesUltimaSemana { get; set; }

        [DataMember]
        public int TransaccionesUltimoMes { get; set; }

        [DataMember]
        public int TransaccionesTotales { get; set; }
    }
}