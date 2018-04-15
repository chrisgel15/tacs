using System;

namespace Tacs.Models
{
    public class Compra : Transaction
    {
        public Compra(User user, Coin coin, int amount, DateTime date) : base(user, coin, amount, date)
        {
        }
    }
}