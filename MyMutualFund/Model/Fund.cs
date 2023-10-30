using MyMutualFund.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace MyMutualFund.Model
{
    public class Fund
    {
        public string TickerSymbol { get; }
        public decimal CashAvailable { get; set; }
        public int QtyOfShares { get; private set; }

        public decimal PricePerShare
        {
            get
            {
                var sumOfShares = PortFolio.Sum(x => x.SharePrice);
                return sumOfShares / QtyOfShares;
            }
        }

        public List<Share> PortFolio { get; set; }

        IStockExchangeCenter _StockExchangeCenter;

        public Fund(string tickerSymbol, IStockExchangeCenter stockExchangeCenter, decimal initialCash)
        {
            TickerSymbol = tickerSymbol;
            PortFolio = new List<Share>();
            this._StockExchangeCenter = stockExchangeCenter;
            this.CashAvailable = initialCash;
        }

        public (bool, Share?) Buy(string symbol)
        {

            var sharePrice = _StockExchangeCenter.CheckPrice(symbol, DateTime.Now).Price;

            if (CashAvailable <= sharePrice) return (false, null);


            var shareBought = _StockExchangeCenter.Sell(symbol);
            if (shareBought != null)
            {
                PortFolio.Add(shareBought);
                CashAvailable -= shareBought.SharePrice;
                QtyOfShares++;
            }

            return (true, shareBought);

        }

        public (bool, Share?) Sell(string symbol)
        {

            var shareToSell = PortFolio.Where(x => { return x.Symbol == symbol; }).ToList().SingleOrDefault();


            if (shareToSell == null)
            {
                return (false, null);
            }

            var buyConfirmed = _StockExchangeCenter.Buy(shareToSell);

            if (buyConfirmed)
            {
                PortFolio.Remove(shareToSell);
                CashAvailable += shareToSell.SharePrice;
                QtyOfShares--;
            }

            return (true, shareToSell);

        }






    }
}
