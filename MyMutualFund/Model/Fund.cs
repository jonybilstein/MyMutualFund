using MyMutualFund.Interfaces;


namespace MyMutualFund.Model
{
    public class Fund
    {
        public string TickerSymbol { get; }
        public decimal CashAvailable { get; set; }
        public int QtyOfShares { get;  set; }

        public decimal PricePerShare(DateTime date)
        {
            var sumOfShares = PortFolio.Sum(x => _StockExchangeCenter.CheckPrice(x.Symbol, date).Price);
            return (sumOfShares + this.CashAvailable) / QtyOfShares;
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

        public (bool, Share?) Buy(string symbol, DateTime date)
        {

            var sharePrice = _StockExchangeCenter.CheckPrice(symbol, DateTime.Now).Price;

            if (CashAvailable <= sharePrice) return (false, null);


            var shareBought = _StockExchangeCenter.Sell(symbol, date);
            if (shareBought != null)
            {
                PortFolio.Add(shareBought);
                CashAvailable -= shareBought.SharePrice;
            }

            return (true, shareBought);

        }

        public (bool, Share?) Sell(string symbol, DateTime date)
        {

            var shareToSell = PortFolio.Where(x => { return x.Symbol == symbol; }).ToList().FirstOrDefault();


            if (shareToSell == null)
            {
                return (false, null);
            }

            var buyConfirmed = _StockExchangeCenter.Buy(shareToSell, date);

            if (buyConfirmed)
            {
                PortFolio.Remove(shareToSell);
                CashAvailable += shareToSell.SharePrice;
            }

            return (true, shareToSell);

        }

        public List<(bool,Share?)> BuyMany (string symbol, DateTime date, int quantity)
        {
            var listOfSharesBought = new List<(bool, Share?)>();
            
            for (int x = 0 ; x < quantity; x++)
            {
                var shareBought = Buy(symbol, date);
                listOfSharesBought.Add(shareBought);
            }

            return listOfSharesBought;
        }

        public List<(bool, Share?)> SellMany(string symbol, DateTime date, int quantity)
        {
            var listOfSharesSold = new List<(bool, Share?)>();

            for (int x = 0; x < quantity; x++)
            {
                var shareSold = Sell(symbol, date);
                listOfSharesSold.Add(shareSold);
            }

            return listOfSharesSold;
        }






    }
}
