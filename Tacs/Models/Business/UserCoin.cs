using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tacs.Models
{
    public class UserCoin
    {
        [Key, Column(Order=0)]
        public int UserID { get; set; }

        [Key, Column(Order = 1)]
        public int CoinID { get; set; }

        public virtual User User { get; set; }
        public virtual Coin Coin { get; set; }


        public int Amount { get; set; }
    }
}