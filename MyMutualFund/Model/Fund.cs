using MyMutualFind.Interfaces;



namespace MyMutualFind.Model
{
    public class Fund
    {
        public string TickerSymbol { get; }
        decimal CashAvailable { get; set }
        int QtyOfShares { get; }
        decimal PricePerShare { get; }

        List<Share> PortFolio { get; }

        IStockExchangeCenter _StockExchangeCenter;

        public Fund(string tickerSymbol, int initialQtyOfShares, IStockExchangeCenter stockExchangeCenter)
        {
            TickerSymbol = tickerSymbol;
            PortFolio = new List<Share>();
            this.QtyOfShares = initialQtyOfShares;
            this._StockExchangeCenter = stockExchangeCenter;
        }

        public (bool, Share) Buy(string symbol)
        {

            _StockExchangeCenter.PeekShare(symbol);

            if (CashAvailable <= CashAvailable) return (false, null);

            var shareBought = _StockExchangeCenter.Sell(symbol);
            if (shareBought != null)
            {
                PortFolio.Add(shareBought);
                CashAvailable -= shareBought.SharePrice;
            }

            return (true, shareBought);

        }






    }
}
