using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.ViewModels
{
    public class UserWallet
    {
        public List<CoinWallet> coinWallets { get; set; }
    }

    public class CoinWallet
    {
        public int WalletId { get; set; }
        public string NombreMoneda { get; set; }
        public Decimal Balance { get; set; }
        public Decimal Cotizacion { get; set; }
    }
}
