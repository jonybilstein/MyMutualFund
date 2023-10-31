using MyMutualFund.Interfaces;
using MyMutualFund.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMutualFund.Model
{
    public class CSVStockPriceChecker : IStockPriceChecker
    {

        private List<StockPrice> StockPrices = new List<StockPrice>();
        public StockPrice? GetPrice(string symbol, DateTime date)
        {
            var stockPriceObj = StockPrices.Where(x => x.TickerSymbol == symbol && x.Date <= date).OrderByDescending(x => x.Date).FirstOrDefault();
            return stockPriceObj;
        }

        public bool LoadCSV(string path)
        {
            using var reader = new StreamReader(path);

            reader.ReadLine(); // title
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                var price = new StockPrice()
                {
                    TickerSymbol = values[1],
                    Date = DateTime.Parse(values[0]),
                    Price = decimal.Parse(values[3])
                };
                StockPrices.Add(price);
            }

            return true;
        }
    }
}
