using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web;

namespace Tacs.Models.Contracts
{
    [DataContract]
    public class UserViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string LastAccessDate { get; set; }

        public UserViewModel(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Password = user.Password;
            LastAccessDate = user.LastAccessDate.ToString();
        }
    }

    [DataContract]
    public class SignupRequest
    {
        [DataMember, Required]
        public string Username { get; set; }

        [DataMember, Required]
        public string Password { get; set; }
    }
}