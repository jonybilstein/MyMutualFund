using Moq;
using MyMutualFund.Interfaces;
using MyMutualFund.Model;
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
            randomShare.Symbol = "LLSA";

            NYSEMock.Setup(x => x.Sell(randomShare.Symbol, It.IsAny<DateTime>())).Returns(randomShare);
            NYSEMock.Setup(x => x.CheckPrice(randomShare.Symbol, It.IsAny<DateTime>())).Returns(new StockPrice { TickerSymbol = randomShare.Symbol, Price = randomShare.SharePrice });


            Fund fund = new Fund("JJLK", NYSEMock.Object, 1000);
            fund.QtyOfShares = 2;
            var x = fund.Buy(randomShare.Symbol, DateTime.Now);

            Assert.IsTrue(fund.CashAvailable + x.Item2.SharePrice == 1000);
            Assert.AreEqual(fund.PortFolio.Single(), randomShare);
        }

        [TestMethod]
        public void BuyShares_DoesNotBuyWhenBalanceIsNegative()
        {
            var randomShare = FundTests.GetRandomShare();

            NYSEMock.Setup(x => x.Sell(randomShare.Symbol, It.IsAny<DateTime>())).Returns(randomShare);
            NYSEMock.Setup(x => x.CheckPrice(randomShare.Symbol, It.IsAny<DateTime>())).Returns(new StockPrice { TickerSymbol = randomShare.Symbol, Price = randomShare.SharePrice });



            Fund fund = new Fund("JJLK", NYSEMock.Object, 8);
            var x = fund.Buy(randomShare.Symbol, DateTime.Now);

            Assert.IsFalse(x.Item1);
            Assert.IsNull(x.Item2);

            Assert.IsTrue(fund.CashAvailable == 8);
            Assert.IsTrue(!fund.PortFolio.Any());

        }

        [TestMethod]
        public void PricePerShare_Correct()
        {
            var randomShareOne = FundTests.GetRandomShare();
            var randomShareTwo = FundTests.GetRandomShare();



            NYSEMock.Setup(x => x.Sell(randomShareOne.Symbol, It.IsAny<DateTime>())).Returns(randomShareOne);
            NYSEMock.Setup(x => x.CheckPrice(randomShareOne.Symbol, It.IsAny<DateTime>())).Returns(new StockPrice { TickerSymbol = randomShareOne.Symbol, Price = randomShareOne.SharePrice });


            NYSEMock.Setup(x => x.Sell(randomShareTwo.Symbol, It.IsAny<DateTime>())).Returns(randomShareTwo);
            NYSEMock.Setup(x => x.CheckPrice(randomShareTwo.Symbol, It.IsAny<DateTime>())).Returns(new StockPrice { TickerSymbol = randomShareTwo.Symbol, Price = randomShareTwo.SharePrice });


            Fund fund = new Fund("JJLK", NYSEMock.Object, 1000);

            var qtyOfShares = 3;
            fund.QtyOfShares = qtyOfShares;

            fund.Buy(randomShareOne.Symbol, DateTime.Now);
            fund.Buy(randomShareTwo.Symbol, DateTime.Now);
            fund.Buy(randomShareTwo.Symbol, DateTime.Now);




            Assert.IsTrue(fund.PricePerShare(DateTime.Now) == (fund.CashAvailable + randomShareOne.SharePrice + (randomShareTwo.SharePrice * 2)) / qtyOfShares);

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

        [TestMethod]
        public void SellShares_UpdatesBalanceAndSharePrice()
        {
            var randomShare = FundTests.GetRandomShare();

            NYSEMock.Setup(x => x.Buy(randomShare, It.IsAny<DateTime>())).Returns(true);
            NYSEMock.Setup(x => x.Sell(randomShare.Symbol, It.IsAny<DateTime>())).Returns(randomShare);
            NYSEMock.Setup(x => x.CheckPrice(randomShare.Symbol, It.IsAny<DateTime>())).Returns(new StockPrice { TickerSymbol = randomShare.Symbol, Price = randomShare.SharePrice });


            Fund fund = new Fund("JJLK", NYSEMock.Object, 10000);
            var boughtShare = fund.Buy(randomShare.Symbol, DateTime.Now);
            var soldShare = fund.Sell(randomShare.Symbol, DateTime.Now);


            Assert.IsTrue(fund.QtyOfShares == 0);
            Assert.IsTrue(fund.CashAvailable == 10000);
            Assert.AreEqual(fund.PortFolio.Any(), false);
        }

        [TestMethod]
        public void SellShares_DoesNotSellNonExistingShare()
        {
            var randomShare = FundTests.GetRandomShare();


            Fund fund = new Fund("JJLK", NYSEMock.Object, 10000);
            var soldShare = fund.Sell("JSTT", DateTime.Now);


            Assert.IsTrue(soldShare.Item1 == false);
            Assert.IsTrue(soldShare.Item2 == null);

        }


    }
}