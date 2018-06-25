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
        public IUnitOfWork _unitOfWork;
        public WalletService()
        {

        }
        public WalletService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<Wallet> VerPortfolio(int userid)
        {
                return _unitOfWork.Users.Get(userid).Wallets.ToList();
        }

        public Wallet GetWalletById(int walletId)
        {
            var context = _unitOfWork;
            return context.Wallets.Get(walletId);
        }

        public Wallet GetWalletByCoinNameAndUser (string coin, int userId)
        {
            var context = _unitOfWork;
            return context.Users.Get(userId).Wallets.Where(w => w.Coin.Name.ToLower() == coin.ToLower()).FirstOrDefault();
        }

        public Wallet GetWalletByCoinNameOrWalletIdAndUser(string coinNameOrWalletId, int userId)
        {
            Wallet wallet;
            if (!coinNameOrWalletId.All(c => Char.IsDigit(c)))
            {
                wallet = GetWalletByCoinNameAndUser(coinNameOrWalletId, userId);
            }
            else
            {
                wallet = GetWalletById(Int32.Parse(coinNameOrWalletId));
            }
            return wallet;
        }

        public async Task<WalletViewModel> GetWalletInfo(Wallet w)
        {
            var wallet = GetWalletById(w.Id);
            var viewModel = new WalletViewModel();
            viewModel.NombreMoneda = wallet.Coin.Name;
            viewModel.Balance = wallet.Balance;
            viewModel.Cotizacion = await wallet.Coin.GetCotizacion();
            viewModel.WalletId = wallet.Id;

            return viewModel;
        }

        public async Task<Wallet> AddWallet(Coin coin, decimal balanceInicial, User usuario)
        {
            var context = _unitOfWork;

            var newWallet = new Wallet(usuario, coin, balanceInicial);

            context.Wallets.Add(newWallet);

            context.Complete();

            return newWallet;
        }
    }
}