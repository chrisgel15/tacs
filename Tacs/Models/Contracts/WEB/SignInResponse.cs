using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tacs.Models.Contracts
{
    [DataContract]
    public class SignInResponse
    {
        [DataMember]
        [Required]
        public bool LoggedIn { get; set; }

        [DataMember]
        [Required]
        public string Message { get; set; }

        [DataMember]
        [Required]
        public bool Error { get; set; }

        [DataMember]
        [Required]
        public DateTime AccessDate { get; set; }
    }
}