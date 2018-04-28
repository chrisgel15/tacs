using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tacs.Models;
using Tacs.Models.Repositories;
using Tacs.Context;
using Tacs.Models.Contracts;
using System.Threading.Tasks;

namespace Tacs.Services
{
    public class WalletService
    {
        public IList<Wallet> VerPortfolio(int userid)
        {
            using (var unitOfWork = new UnitOfWork(new TacsDataContext()))
            {
                return unitOfWork.Users.Get(userid).Wallets.ToList();
            }
        }

        public Wallet GetWalletById(int walletId)
        {
            var context = new UnitOfWork(new TacsDataContext());
            return context.Wallets.Get(walletId);
        }

        public async Task<WalletViewModel> GetWalletInfo(Wallet w)
        {
            var wallet = new WalletService().GetWalletById(w.Id);
            var viewModel = new WalletViewModel();
            viewModel.NombreMoneda = wallet.Coin.Name;
            viewModel.Balance = wallet.Balance;
            viewModel.Cotizacion = await wallet.Coin.GetCotizacion();
            viewModel.WalletId = wallet.Id;

            return viewModel;
        }

        public async Task<Wallet> AddWallet(NewWalletRequest newWalletRequest, int ownerId)
        {
            //TODO: Agregar validacion del nombre de moneda (que este en CMC)
            var context = new UnitOfWork(new TacsDataContext());

            var newWallet = new Wallet(context.Users.Get(ownerId), context.Coins.Get(CoinService.GetCoinId(newWalletRequest.NombreMoneda)), newWalletRequest.Balance);

            context.Wallets.Add(newWallet);

            context.Complete();

            return  newWallet;

        }
    }
}