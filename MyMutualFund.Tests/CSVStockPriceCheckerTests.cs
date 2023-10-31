using MyMutualFund.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMutualFund.Tests
{

    [TestClass]
    public class CSVStockPriceCheckerTests
    {
        [TestMethod]
        public void CSVStockPriceChecker_LoadsCsvAndReturnsTrue()
        {

            var path = Directory.GetCurrentDirectory() + @"\Source\StockPrices.csv";
            var csv_checker = new CSVStockPriceChecker();
            var load_success = csv_checker.LoadCSV(path);
            Assert.IsTrue(load_success);
        }

        [TestMethod]
        public void CSVStockPriceChecker_CheckPrice_ReturnsAccurateResult()
        {
            var path = Directory.GetCurrentDirectory() + @"\Source\StockPrices.csv";
            var csv_checker = new CSVStockPriceChecker();
            csv_checker.LoadCSV(path);

            var priceInfo = csv_checker.GetPrice("WLTW", DateTime.Parse("2016-01-07 02:00:00"));
            Assert.IsTrue(priceInfo.Price == Decimal.Parse("119.980003"));

        }

    }
}
