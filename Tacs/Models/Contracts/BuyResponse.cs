using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Tacs.Controllers
{
    public class BuyResponse
    {
        [DataMember]
        [Required]
        public string Message { get; set; }

        [DataMember]
        [Required]
        public bool Error { get; set; }
    }
}