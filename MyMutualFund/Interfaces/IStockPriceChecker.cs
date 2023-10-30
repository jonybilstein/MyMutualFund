using MyMutualFund.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMutualFund.Interfaces
{
    public interface IStockPriceChecker
    {
        
        StockPrice? GetPrice(string symbol, DateTime date);

    }
}
