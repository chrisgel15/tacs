using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tacs.Models.Contracts
{
    [DataContract]
    public class UserInfoResponse
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int NumberOfUserCoins { get; set; }

        [DataMember]
        public int Transactions { get; set; }

        [DataMember]
        public string LastAccess { get; set; }

        public UserInfoResponse(User user)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.NumberOfUserCoins = user.UserCoins.Count;
            this.Transactions = user.UserCoins.Sum(uc => uc.Transactions.Count);
            this.LastAccess = DateTime.Now.ToString();
        }
    }
}