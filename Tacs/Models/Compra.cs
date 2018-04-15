using System;

namespace Tacs.Models
{
    public class Compra : Transaction
    {
        public Compra(User user, Coin coin, int amount, DateTime date, decimal price) : base(user, coin, amount, date, price)
        {
        }
    }
}