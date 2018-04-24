using System;

namespace Tacs.Models
{
    public class Venta : Transaction
    {
        private Venta()
        {

        }
        public Venta(User user, Coin coin, int amount, DateTime date, decimal price) : base(user, coin, amount, date, price)
        {
        }
    }
}