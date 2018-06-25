using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;


namespace Tacs.Models.Contracts
{
    [DataContract]
    public class AdminComparisonRequest
    {
        [DataMember]
        public String User1 { get; set; }

        [DataMember]
        public String User2 { get; set; }
    }


    [DataContract]
    public class UserComparisonResponse
    {
        [DataMember]
        public String User1 { get; set; }
        [DataMember]
        public String User2 { get; set; }
        [DataMember]
        public decimal Patrimonio1 { get; set; }
        [DataMember]
        public decimal Patrimonio2 { get; set; }   
    }

    [DataContract]
    public class AdminUserInfoResponse
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int NumberOfWallets { get; set; }

        [DataMember]
        public int Transactions { get; set; }

        [DataMember]
        public string LastAccess { get; set; }

        public AdminUserInfoResponse(User user)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.NumberOfWallets = user.Wallets.Count(w => w.Balance > 0);
            this.Transactions = user.Wallets.Sum(uc => uc.Transactions.Count);
            this.LastAccess = user.LastAccessDate.ToString();
        }
    }

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