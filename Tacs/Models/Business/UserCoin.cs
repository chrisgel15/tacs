using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Tacs.Models
{
    public class UserCoin
    {
        [Key]
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Coin Coin { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public int Amount { get; set; }
    }
}