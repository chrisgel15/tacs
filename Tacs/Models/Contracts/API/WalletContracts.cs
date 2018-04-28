using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Tacs.Models.Contracts
{
    [DataContract]
    public class WalletViewModel
    {
        [DataMember]
        [Required]
        public int WalletId { get; set; }

        [DataMember]
        [Required]
        public string NombreMoneda { get; set; }

        [DataMember]
        [Required]
        public decimal Balance { get; set; }

        [DataMember]
        [Required]
        public decimal Cotizacion { get; set; }
    }

    public class NewWalletRequest
    {
        //TODO: verificar que este en CoinMarketCap
        [DataMember]
        [Required]
        public string NombreMoneda { get; set; }

        [DataMember]
        [Required]
        public decimal Balance { get; set; }
    }

}