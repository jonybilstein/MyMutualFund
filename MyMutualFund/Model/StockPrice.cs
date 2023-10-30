using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMutualFund.Model
{

    public class StockPrice
    {
        public string TickerSymbol { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }

    }

}
