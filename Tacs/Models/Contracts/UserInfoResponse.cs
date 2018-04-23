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
        public ICollection<UserCoin> userCoins { get; set; }

        [DataMember]
        public string lastAccess { get; set; }

        public UserInfoResponse(int _id, string _name, ICollection<UserCoin> _userCoins)
        {
            this.id = _id;
            this.name = _name;
            this.userCoins = _userCoins;
            this.lastAccess = DateTime.Now.ToString();
        }
    }
}