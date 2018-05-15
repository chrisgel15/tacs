using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Tacs.Services;

namespace Tacs.Models
{
    [DataContract]
    public class Token
    {
        protected Token()
        {

        }

        public Token(string token)
        {
            this.SessionToken = token;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Index(IsUnique = true)]
        [Required]
        public string SessionToken { get; set; }

        [DataMember]
        public virtual User User { get; set; }

    }
}