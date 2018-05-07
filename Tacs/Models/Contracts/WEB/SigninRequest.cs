using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tacs.Models.Contracts
{
    [DataContract]
    public class SignInRequest
    {
        [DataMember]
        [Required]
        public User User { get; set; }

        [DataMember]
        [Required]
        public AccessKeys AccessKeys { get; set; }
    }
}