using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tacs.Models.DTO
{
    [DataContract]
    public class SignupRequest
    {
        [DataMember]
        [Required]
        public User user { get; set; }

        [DataMember]
        [Required]
        public AccessKeys accessKeys { get; set; }
    }
}