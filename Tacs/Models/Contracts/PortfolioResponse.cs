using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Tacs.Models.Contracts
{
    [DataContract]
    public class PortfolioResponse
    {
        [DataMember]
        [Required]
        public string NombreMoneda { get; set; }

        [DataMember]
        [Required]
        public decimal Cotizacion { get; set; }

        public PortfolioResponse(String nombre, decimal cotizacion)
        {
            NombreMoneda = nombre;
            Cotizacion = cotizacion;
        }
        public PortfolioResponse()
        {

        }
    }
}