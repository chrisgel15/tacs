using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tacs.Models.Contracts
{
    [DataContract]
    public class NewTransactionRequest
    {
        [DataMember]
        [Required]
        public string Type { get; set; }

        [DataMember]
        [Required]
        public decimal Amount { get; set; }
    }

    [DataContract]
    public class NewTransactionResponse
    {
        [DataMember]
        [Required]
        public string Message { get; set; }

        [DataMember]
        [Required]
        public bool Error { get; set; }
    }

    [DataContract]
    public class TransactionViewModel
    {
        [DataMember]
        [Required]
        public int TransactionId { get; set; }

        [DataMember]
        [Required]
        public string Type { get; set; }

        [DataMember]
        [Required]
        public decimal Amount { get; set; }

        [DataMember]
        [Required]
        public string Date { get; set; }

        [DataMember]
        [Required]
        public decimal Price { get; set; }

        [DataMember]
        [Required]
        public string Coin { get; set; }

        [DataMember]
        [Required]
        public string User { get; set; }

    }
}