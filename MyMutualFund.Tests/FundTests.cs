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
        
        
        [TestInitialize]
        public void SetUp()
        {

            NYSEMock.Setup(x => x.Sell(It.IsAny<string>())).Returns(GetShare());
           
        }

        [TestMethod]
        public void BuyShares_UpdatesBalanceAndSharePrice()
        {
            Fund fund = new Fund("JJLK", NYSEMock.Object, 200000);
            
            var x = fund.Buy("AAJL");
            Assert.IsTrue(fund.QtyOfShares == 1);
            Assert.IsTrue(fund.CashAvailable + x.Item2.SharePrice == 200000);

        }

        public static Share GetShare()
        {
            var rand = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var shareId = new string(Enumerable.Repeat(0, 10).Select(_ => chars[rand.Next(chars.Length)]).ToArray());
            var shareSymbol = new string(Enumerable.Repeat(0, 4).Select(_ => chars[rand.Next(chars.Length)]).ToArray());



            var randomSharePrice = new Decimal(rand.NextDouble() * (500 - 1) + 1);


            var shareToReturn = new Share()
            {
                ShareId = shareId,
                SharePrice = randomSharePrice,
                Symbol = shareSymbol
            };

            return shareToReturn;



        }

        public static string GetRandomSymbol(string symbol)
        {
            var rand = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var shareSymbol = new string(Enumerable.Repeat(0, 4).Select(_ => chars[rand.Next(chars.Length)]).ToArray());

            return shareSymbol;



        }


    }
}