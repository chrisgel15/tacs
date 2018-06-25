using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tacs.Models;
using Tacs.Models.Exceptions;
using Tacs.Services;

namespace Tacs.Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void UserHasNamePasswordAndEmptyWallet()
        {
            User user = new User("name", "password");
            user.Id = 1;

            Assert.AreEqual("name", user.Name);
            Assert.AreEqual("password", user.Password);
            Assert.IsNotNull(user.Wallets);
            Assert.AreEqual(0, user.Wallets.Count);
        }

        [TestMethod]
        public void UserHasWallet()
        {
            const string coinName = "bitcoin";
            User user = new User("u", "p");
            user.Id = 1;
            Coin coin = new Coin(coinName, 1);

            Assert.IsTrue(user.Wallets.Count == 0, "User should have a empty wallet");

            user.Buy(coin, 50);

            Assert.IsTrue(user.Wallets.Count == 1, "User should have a wallet");
        }

        [TestMethod]
        public void UserBuysAmount()
        {
            const string coinName = "bitcoin";
            User user = new User("u", "p");
            user.Id = 1;
            Coin coin = new Coin(coinName, 1);
            user.Buy(coin, 50);

            Assert.AreEqual(1, user.Wallets.Count);
            Wallet userCoin = user.Wallets.First(uc => uc.Coin.Id == coin.Id);
            Assert.AreEqual(50, userCoin.Balance);

            user.Buy(coin, 100);

            Assert.AreEqual(1, user.Wallets.Count);
            userCoin = user.Wallets.First(uc => uc.Coin.Id == coin.Id);
            Assert.AreEqual(150, user.Wallets.Sum(u => u.Balance));
        }
        [TestMethod]
        public void PasswordEncription()
        {
            SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();
            var hashedPassword1 = System.Text.Encoding.Default.GetString(provider.ComputeHash(Encoding.UTF8.GetBytes("password")));
            var hashedPassword2 = System.Text.Encoding.Default.GetString(provider.ComputeHash(Encoding.UTF8.GetBytes("password")));
            var hashedPassword3 = System.Text.Encoding.Default.GetString(provider.ComputeHash(Encoding.UTF8.GetBytes("passwor1")));

            Assert.AreEqual(hashedPassword1, hashedPassword2);
            Assert.AreNotEqual(hashedPassword1, hashedPassword3);
        }
        [TestMethod]
        public void UserSellsAmount()
        {
            const string coinName = "cryptocoin";
            User user = new User("u", "p");
            Coin coin = new Coin(coinName);
            user.Buy(coin, 500);

            user.Sell(coin, 80);

            Assert.AreEqual(1, user.Wallets.Count);
            Wallet userCoin = user.Wallets.First(uc => uc.Coin.Id == coin.Id);
            Assert.AreEqual(420, user.Wallets.Sum(uc => uc.Balance));

            user.Sell(coin, 120);

            Assert.AreEqual(1, user.Wallets.Count);
            userCoin = user.Wallets.First(uc => uc.Coin.Id == coin.Id);
            Assert.AreEqual(300, user.Wallets.Sum(uc => uc.Balance));
        }

        [TestMethod]
        public void UserSellsAndBuyAmount()
        {
            const string coinName = "digicoin";
            User user = new User("u", "p");
            Coin coin = new Coin(coinName);
            user.Buy(coin, 500);

            Assert.AreEqual(1, user.Wallets.Count);
            Wallet usercoin = user.Wallets.First(uc => uc.Coin.Name == coin.Name);
            Assert.AreEqual(500, user.Wallets.Sum(uc => uc.Balance));

            user.Sell(coin, 75);

            Assert.AreEqual(1, user.Wallets.Count);
            usercoin = user.Wallets.First(uc => uc.Coin.Name == coin.Name);
            Assert.AreEqual(425, user.Wallets.Sum(uc => uc.Balance));
        }

        [TestMethod]
        public void UserHasTwoDifferentCoins()
        {
            // TODO
            const string coinName = "cryptocoin";
            const string anotherCoinName = "digitcoin";
            User user = new User("u", "p");
            Coin coin = new Coin(coinName, 1);
            Coin anotherCoin = new Coin(anotherCoinName, 2);

            user.Buy(coin, 1000);

            //Assert.AreEqual(1, user.Wallets.Count);
            //Wallet w = user.Wallets.First(d => d.Coin.Name == coinName);
            //Assert.AreEqual(w.Amount, 1000);

            user.Buy(anotherCoin, 600);

            //Assert.AreEqual(2, user.Wallets.Count);
            //Wallet wa = user.Wallets.First(d => d.Coin.Name == anotherCoinName);
            //Assert.AreEqual(wa.Amount, 600);

            user.Sell(coin, 120);

            //Assert.AreEqual(2, user.Wallets.Count);
            //w = user.Wallets.First(d => d.Coin.Name == coinName);
            //Assert.AreEqual(w.Amount, 880);

            user.Sell(anotherCoin, 300);

            //Assert.AreEqual(2, user.Wallets.Count);
            //wa = user.Wallets.First(d => d.Coin.Name == anotherCoinName);
            //Assert.AreEqual(wa.Amount, 300);
        }

        [TestMethod]
        public void UserTriesToSellMoreThanHeHas()
        {
            const string coinName = "coindigit";
            User user = new User("u", "p");
            Coin coin = new Coin(coinName, 1);
            user.Buy(coin, 45);

            Assert.ThrowsException<InsufficientAmountException>(() => user.Sell(coin, 50));
        }

        [TestMethod]
        public void UserTriesToSellACoinHeDoesNotOwn()
        {
            const string coinName = "coindigit", anotherCoin = "otherCoin";
            User user = new User("u", "p");
            Coin coin = new Coin(coinName, 1);
            Coin other = new Coin(anotherCoin, 2);

            user.Buy(coin, 45);

            Assert.ThrowsException<InvalidOperationException>(() => user.Sell(other, 50));
        }

        [TestMethod]
        public void VerPortfolioDeUsuarioPorId()
        {
            const string _bitcoin = "Bitcoin";
            const string _ethereum = "Ethereum";
            const string _ripple = "Ripple";
            const string _eos = "EOS";

            User unUsuario = new User("unUsuario", "unPassword");
            User otroUsuario = new User("otroUsuario","otroPassword");
            unUsuario.Id = 2;
            otroUsuario.Id = 1;
            

            Coin coin1 = new Coin(_bitcoin, 1);
            Coin coin2 = new Coin(_ethereum, 2);
            Coin coin3 = new Coin(_ripple, 3);
            Coin coin4 = new Coin(_eos, 4);

            Wallet wallet = new Wallet(unUsuario, coin1, 0);
            Wallet otroWallet = new Wallet(otroUsuario, coin2, 0);
            Wallet wallet2 = new Wallet(otroUsuario, coin3, 0);
            Wallet wallet3 = new Wallet(otroUsuario, coin4, 0);

            List<Wallet> wallets = new List<Wallet>();
            wallets.Add(otroWallet);
            wallets.Add(wallet2);
            wallets.Add(wallet3);
            wallets.Add(wallet);

            var otroUsuarioWallets = wallets.Where(w => w.User.Id == otroUsuario.Id);
            var unUsuarioWallets = wallets.Where(w => w.User.Id == unUsuario.Id);

            Assert.AreEqual(4, wallets.Count());
            Assert.AreEqual(3, otroUsuarioWallets.Count());
            Assert.AreEqual(1, unUsuarioWallets.Count());

        }

        [TestMethod]
        public void UsuarioTieneCeroPatrimonio()
        {
            User user = new User("user", "p");
            Task<decimal> value = user.GetPatrimonio();
            Task.WaitAll();
            Assert.AreEqual(value.Result, 0);
        }

        [TestMethod]
        public void UsuarioTienePatrimonio()
        {
            User user = new User(1, "user", "p");
            Coin coin = new Coin("bitcoin", 1);
            user.Buy(coin, 1);
            Task<decimal> value = user.GetPatrimonio();
            Task.WaitAll();
            Assert.IsTrue(value.Result > 0);
        }

        [TestMethod]
        public void MonedaTieneCotizacion()
        {
            Coin coin = new Coin("bitcoin", 1);
            Task<decimal> value = coin.GetCotizacion();
            Task.WaitAll();
            Assert.IsTrue(value.Result > 0);

        }

        [TestMethod]
        public void WalletTieneBalanceInicial()
        {
            User user = new User(1, "user", "p");
            Coin coin = new Coin("bitcoin", 1);

            Wallet wallet = new Wallet(user, coin, 10);

            Assert.AreEqual(10, wallet.Balance);

        }

        [TestMethod]
        public void WalletTieneTransacciones()
        {
            User user = new User(1, "user", "p");
            Coin coin = new Coin("bitcoin", 1);

            Wallet wallet = new Wallet(user, coin, 10);

            Assert.IsTrue(wallet.Transactions != null);

        }

    }     


}
