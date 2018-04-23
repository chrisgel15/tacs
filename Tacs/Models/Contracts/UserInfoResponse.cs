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
        public int id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public IList<Wallet> wallets { get; set; }

        [DataMember]
        public string lastAccess { get; set; }

        public UserInfoResponse(int _id, string _name, IList<Wallet> _wallets)
        {
            this.id = _id;
            this.name = _name;
            this.wallets = _wallets;
            this.lastAccess = DateTime.Now.ToString();
        }
    }
}