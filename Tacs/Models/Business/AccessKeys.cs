using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tacs.Models
{
    // TODO: This is a skeleton to get the access keys for every request.
    [DataContract]
    public class AccessKeys
    {
        [DataMember]
        [Required]
        public string Key { get; set; }
    }
}