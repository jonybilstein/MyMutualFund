using Moq;
using MyMutualFind.Interfaces;
using MyMutualFind.Model;
using System;

namespace MyMutualFund.Tests
{
    [TestClass]
    public class FundTests
    {


        static Mock<IStockExchangeCenter> NYSEMock = new Mock<IStockExchangeCenter>();
        static Random Rand = new Random();

        [TestInitialize]
        public void SetUp()
        {



        }

        [TestMethod]
        public void BuyShares_UpdatesBalanceAndSharePrice()
        {

            var randomShare = FundTests.GetRandomShare();

            NYSEMock.Setup(x => x.Sell(randomShare.Symbol)).Returns(randomShare);
            NYSEMock.Setup(x => x.PeekShare(randomShare.Symbol)).Returns(randomShare);


            Fund fund = new Fund("JJLK", NYSEMock.Object, 200000);
            var x = fund.Buy(randomShare.Symbol);

            Assert.IsTrue(fund.QtyOfShares == 1);
            Assert.IsTrue(fund.CashAvailable + x.Item2.SharePrice == 200000);
            Assert.AreEqual(fund.PortFolio.Single(), randomShare);
        }

        [TestMethod]
        public void BuyShares_DoesNotBuyWhenBalanceIsNegative()
        {
            var randomShare = FundTests.GetRandomShare();

            NYSEMock.Setup(x => x.Sell(randomShare.Symbol)).Returns(randomShare);
            NYSEMock.Setup(x => x.PeekShare(randomShare.Symbol)).Returns(randomShare);


            Fund fund = new Fund("JJLK", NYSEMock.Object, 8);
            var x = fund.Buy(randomShare.Symbol);

            Assert.IsFalse(x.Item1);
            Assert.IsNull(x.Item2);
            
            Assert.IsTrue(fund.QtyOfShares == 0);
            Assert.IsTrue(fund.CashAvailable == 8);
            Assert.IsTrue(!fund.PortFolio.Any());

        }

        public static Share GetRandomShare()
        {
            var shareId = FundTests.GetRandomLetters(10);
            var shareSymbol = FundTests.GetRandomLetters(4);
            var sharePrice = FundTests.GetRandomPrice(10, 51);


            var shareToReturn = new Share()
            {
                ShareId = shareId,
                SharePrice = sharePrice,
                Symbol = shareSymbol
            };

            return shareToReturn;



        }

        public static string GetRandomLetters(int length)
        {

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var shareSymbol = new string(Enumerable.Repeat(0, length).Select(_ => chars[Rand.Next(chars.Length)]).ToArray());

            return shareSymbol;

        }


        public static decimal GetRandomPrice(double min, double max)
        {

            return new Decimal(Rand.NextDouble() * (max - min) + 1);

        }


    }
}