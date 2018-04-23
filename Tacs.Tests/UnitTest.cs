using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tacs.Models;
using Tacs.Models.Exceptions;

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
            Assert.IsNotNull(user.UserCoins);
            Assert.AreEqual(0, user.UserCoins.Count);
        }

        [TestMethod]
        public void UserBuysAmount()
        {
            const string coinName = "bitcoin";
            User user = new User("u", "p");
            user.Id = 1;
            Coin coin = new Coin(coinName, 1);
            user.Buy(coin, 50);

            Assert.AreEqual(1, user.UserCoins.Count);
            UserCoin userCoin = user.UserCoins.First(uc => uc.Coin.Id == coin.Id);
            Assert.AreEqual(50, userCoin.Amount);

      //      Assert.AreEqual(1, user.Wallets.Count);
      //      Wallet w = user.Wallets.First(d => d.Coin.Name == coinName);
       //     Assert.AreEqual(50, w.Amount);

            user.Buy(coin, 100);

            Assert.AreEqual(1, user.UserCoins.Count);
            userCoin = user.UserCoins.First(uc => uc.Coin.Id == coin.Id);
            Assert.AreEqual(150, userCoin.Amount);

            //            Assert.AreEqual(1, user.Wallets.Count);
            //          w = user.Wallets.First(d => d.Coin.Name == coinName);
            //        Assert.AreEqual(150, w.Amount);
        }

        [TestMethod]
        public void UserSellsAmount()
        {
            const string coinName = "cryptocoin";
            User user = new User("u", "p");
            Coin coin = new Coin(coinName);
            user.Buy(coin, 500);


            user.Sell(coin, 80);

            //Assert.AreEqual(1, user.Wallets.Count);
            //Wallet w = user.Wallets.First(d => d.Coin.Name == coinName);
            //Assert.AreEqual(w.Amount, 420);

            user.Sell(coin, 120);

            //Assert.AreEqual(1, user.Wallets.Count);
            //w = user.Wallets.First(d => d.Coin.Name == coinName);
            //Assert.AreEqual(w.Amount, 300);
        }

        [TestMethod]
        public void UserSellsAndBuyAmount()
        {
            const string coinName = "digicoin";
            User user = new User("u", "p");
            Coin coin = new Coin(coinName);
            user.Buy(coin, 500);

            //Assert.AreEqual(1, user.Wallets.Count);
            //Wallet w = user.Wallets.First(d => d.Coin.Name == coinName);
            //Assert.AreEqual(w.Amount, 500);

            user.Sell(coin, 75);

            //Assert.AreEqual(1, user.Wallets.Count);
            //w = user.Wallets.First(d => d.Coin.Name == coinName);
            //Assert.AreEqual(w.Amount, 425);
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
            Coin coin = new Coin(coinName);
            user.Buy(coin, 45);

            //Assert.AreEqual(1, user.Wallets.Count);
            //Wallet w = user.Wallets.First(d => d.Coin.Name == coinName);
            //Assert.AreEqual(w.Amount, 45);

            Assert.ThrowsException<InsufficientAmountException>(() => user.Sell(coin, 50));
            //Assert.AreEqual(w.Amount, 45);  
        }     
    }
}
