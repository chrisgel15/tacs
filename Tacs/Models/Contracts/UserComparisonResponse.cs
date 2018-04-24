using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;


namespace Tacs.Models.Contracts
{
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

        public UserComparisonResponse(User user1, User user2)
        {
            User1 = user1.Name;
            Patrimonio1 = user1.GetPatrimonio();
            User2 = user2.Name;
            Patrimonio2 = user2.GetPatrimonio();
        }
    
    }
}