using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tacs.Models.Contracts
{
    [DataContract]
    public class GetTransactionsByCoinResponse
    {
        public GetTransactionsByCoinResponse(IList<Transaction> transactions)
        {
            Transactions = new List<TransactionWithTypeResponse>();
            foreach(var t in transactions)
            {
                Transactions.Add(new TransactionWithTypeResponse(t));
            }
        }
        [DataMember]
        [Required]
        public IList<TransactionWithTypeResponse> Transactions { get; set; }
        [DataMember]
        [Required]
        public bool Error { get; set; }
        [DataMember]
        [Required]
        public int CoinId { get; set; }
    }

    public class TransactionWithTypeResponse
    {
        public TransactionWithTypeResponse(Transaction transaction)
        {
            this.Transaction = transaction;
            this.Type = transaction.GetType().ToString();
        }

        [DataMember]
        [Required]
        public Transaction Transaction { get; set; }

        [DataMember]
        [Required]
        public string Type { get; set; }
    }
}