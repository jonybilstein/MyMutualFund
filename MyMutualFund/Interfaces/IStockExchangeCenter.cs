using MyMutualFund.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMutualFund.Interfaces
{
    public interface IStockExchangeCenter
    {
        IStockPriceChecker Checker { get; set; }
        Share Sell(string tickerSymbol);
        StockPrice CheckPrice(string tickerSymbol, DateTime date);
        bool Buy(Share share);
    }
}
