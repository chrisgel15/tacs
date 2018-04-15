using System;

namespace Tacs.Models
{
    public class Venta : Transaction
    {
        public Venta(User user, Coin coin, int amount, DateTime date) : base(user, coin, amount, date)
        {
        }
    }
}