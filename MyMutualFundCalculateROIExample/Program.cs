// See https://aka.ms/new-console-template for more information



using MyMutualFund.Interfaces;
using MyMutualFund.Model;




var csvPriceCheker = new CSVStockPriceChecker();
csvPriceCheker.LoadCSV(Directory.GetCurrentDirectory() + @"\source\prices.csv");
IStockExchangeCenter NYSE = new FakeStockExchangeCenter(csvPriceCheker);
var myFund = new Fund("MMF", NYSE, 100000);
myFund.QtyOfShares = 200;



var Q1 = DateTime.Parse("2016-03-15");
var Q2 = DateTime.Parse("2016-06-15");
var Q3 = DateTime.Parse("2016-09-15");
var Q4 = DateTime.Parse("2016-12-15");




//Q1 

Console.WriteLine("---Buy 1000 shares of AAPL,JPM,MSFT in Q1---");

for (int x = 0; x < 1000; x++)
{
    myFund.Buy("AAPL", Q1);
    myFund.Buy("JPM", Q1);
    myFund.Buy("MSFT", Q1);

}

Console.WriteLine($"Cash available in Q1: {myFund.CashAvailable.ToString()}");
var Q1_PricePerShare = myFund.PricePerShare(Q1);
Console.WriteLine($"Price per share:{Q1_PricePerShare}");
Console.WriteLine();



// Q2 
Console.WriteLine("Sell 300 shares of AAPL and MSFT, Buy 300 shares of GS (Goldman Sachs) and C (Citigroup)");

for (int x = 0; x < 300; x++)
{
    myFund.Sell("AAPL", Q1);
    myFund.Sell("MSFT", Q1);
    myFund.Buy("GS", Q1);
    myFund.Buy("C", Q1);
}

Console.WriteLine($"Cash available in Q2: {myFund.CashAvailable}");
var Q2_PricePerShare = myFund.PricePerShare(Q2);
Console.WriteLine($"Price per share:{Q2_PricePerShare}");
Console.WriteLine();



// Q3 Share Price
Console.WriteLine($"Cash available in Q3: {myFund.CashAvailable}");
var Q3_PricePerShare = myFund.PricePerShare(Q3);
Console.WriteLine($"Price per share:{Q3_PricePerShare}");
Console.WriteLine();


// Q4 Share Price
Console.WriteLine($"Cash available in Q4: {myFund.CashAvailable}");
var Q4_PricePerShare = myFund.PricePerShare(Q4);
Console.WriteLine($"Price per share:{Q4_PricePerShare}");
Console.WriteLine();

// Final ROI


