using MyMutualFund.Interfaces;
using MyMutualFund.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyMutualFund.Model
{
    public class FakeStockExchangeCenter : IStockExchangeCenter
    {
        public IStockPriceChecker Checker { get; set; }

        private static int _ShareId = 2124;

        private static int ShareId
        {
            get
            {
                _ShareId++;
                return _ShareId;
            }
        }

        public FakeStockExchangeCenter(IStockPriceChecker checker)
        {
            this.Checker = checker;
        }


        public bool Buy(Share share, DateTime date)
        {
            var sharePrice = this.Checker.GetPrice(share.Symbol, date);

            if (sharePrice == null) return false;

            return true;


        }

        public StockPrice? CheckPrice(string tickerSymbol, DateTime date)
        {
            return this.Checker.GetPrice(tickerSymbol, date);
        }

        public Share Sell(string tickerSymbol, DateTime date)
        {
            var sharePrice = this.Checker.GetPrice(tickerSymbol, date);

            if (sharePrice == null) return null;

            return new Share
            {
                ShareId = ShareId.ToString(),
                SharePrice = sharePrice.Price,
                Symbol = tickerSymbol
            };
        }
    }
}
