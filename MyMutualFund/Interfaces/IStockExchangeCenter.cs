using MyMutualFind.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMutualFind.Interfaces
{
    public interface IStockExchangeCenter
    {
        Share Sell(string tickerSymbol);
        Share PeekShare(string tickerSymbol);
        
    }
}
