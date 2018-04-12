using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tacs.Models;

namespace Tacs.Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void UserHasNamePasswordAndEmptyWallet()
        {
            User user = new User("name", "password");

            Assert.AreEqual("name", user.Name);
            Assert.AreEqual("password", user.Password);
            Assert.IsNotNull(user.Wallets);
            Assert.AreEqual(0, user.Wallets.Count);
        }

        [TestMethod]
        public void UserBuysAmount()
        {
            const string coinName = "bitcoin";
            User user = new User("u", "p");
            Coin coin = new Coin(coinName);
            user.Buy(coin, 50);

            Assert.AreEqual(1, user.Wallets.Count);
            Wallet w = user.Wallets.First(d => d.Coin.Name == coinName);
            Assert.AreEqual(50, w.Amount);

            user.Buy(coin, 100);

            Assert.AreEqual(1, user.Wallets.Count);
            w = user.Wallets.First(d => d.Coin.Name == coinName);
            Assert.AreEqual(150, w.Amount);
        }

        [TestMethod]
        public void UserSellsAmount()
        {
            const string coinName = "cryptocoin";
            User user = new User("u", "p");
            Coin coin = new Coin(coinName);
            user.Sell(coin, 80);

            Assert.AreEqual(1, user.Wallets.Count);
            Wallet w = user.Wallets.First(d => d.Coin.Name == coinName);
            Assert.AreEqual(w.Amount, -80);

            user.Sell(coin, 120);

            Assert.AreEqual(1, user.Wallets.Count);
            w = user.Wallets.First(d => d.Coin.Name == coinName);
            Assert.AreEqual(w.Amount, -200);
        }

        [TestMethod]
        public void UserSellsAndBuyAmount()
        {
            const string coinName = "digicoin";
            User user = new User("u", "p");
            Coin coin = new Coin(coinName);
            user.Sell(coin, 500);

            Assert.AreEqual(1, user.Wallets.Count);
            Wallet w = user.Wallets.First(d => d.Coin.Name == coinName);
            Assert.AreEqual(w.Amount, -500);

            user.Buy(coin, 75);

            Assert.AreEqual(1, user.Wallets.Count);
            w = user.Wallets.First(d => d.Coin.Name == coinName);
            Assert.AreEqual(w.Amount, -425);
        }

        [TestMethod]
        public void UserHasTwoDifferentCoins()
        {
            // TODO
            const string coinName = "cryptocoin";
            User user = new User("u", "p");
            Coin coin = new Coin(coinName);
            user.Sell(coin, 80);

            Assert.AreEqual(1, user.Wallets.Count);
            Wallet w = user.Wallets.First(d => d.Coin.Name == coinName);
            Assert.AreEqual(w.Amount, -80);

            user.Sell(coin, 120);

            Assert.AreEqual(1, user.Wallets.Count);
            w = user.Wallets.First(d => d.Coin.Name == coinName);
            Assert.AreEqual(w.Amount, -200);
        }
    }
}
