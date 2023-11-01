// See https://aka.ms/new-console-template for more information



using MyMutualFund.Interfaces;
using MyMutualFund.Model;




var csvPriceCheker = new CSVStockPriceChecker();
csvPriceCheker.LoadCSV(Directory.GetCurrentDirectory() + @"\source\prices.csv");
IStockExchangeCenter NYSE = new FakeStockExchangeCenter(csvPriceCheker);
var myFund = new Fund("MMF", NYSE, 60000);
myFund.QtyOfShares = 200;


//Q1 

Console.WriteLine("---Buy 100 shares of AAPL,JPM,MSFT in Q1---");

var Q1 = DateTime.Parse("2016-03-15");
var Q1Shares = myFund.BuyMany("AAPL", Q1, 100)
    .Concat(myFund.BuyMany("JPM", Q1, 100))
    .Concat(myFund.BuyMany("MSFT", Q1, 100))
    .ToList();

if (Q1Shares.Any(x => x.Item1 == false))
{
    Console.WriteLine("Error in Q1. Some shares could not be bought");
    Console.ReadLine();
    return;
}



Console.WriteLine($"Cash available in Q1: {myFund.CashAvailable.ToString()}");
Console.WriteLine($"Price per share:{myFund.PricePerShare(Q1)}");
Console.WriteLine();



// Q2 
Console.WriteLine("Sell 25 shares of AAPL and MSFT, Buy 200 shares of GS (Goldman Sachs) and C (Citigroup)");

var Q2 = DateTime.Parse("2016-06-15");
var Q2Shares = myFund.SellMany("AAPL", Q1, 25)
    .Concat(myFund.SellMany("MSFT", Q1, 25))
    .Concat(myFund.BuyMany("GS", Q1, 200))
    .Concat(myFund.BuyMany("C", Q1, 200));


if (Q1Shares.Any(x => x.Item1 == false))
{
    Console.WriteLine("Error in Q2. Some shares could not be bought or sold");
    Console.ReadLine();
    return;
}

Console.WriteLine($"Cash available in Q2: {myFund.CashAvailable}");
Console.WriteLine($"Price per share:{myFund.PricePerShare(Q2)}");
Console.WriteLine();



// Q3 Share Price
var Q3 = DateTime.Parse("2016-09-15");
Console.WriteLine($"Cash available in Q3: {myFund.CashAvailable}");
Console.WriteLine($"Price per share:{myFund.PricePerShare(Q3)}");
Console.WriteLine();


// Q4 Share Price
var Q4 = DateTime.Parse("2016-12-15");
Console.WriteLine($"Cash available in Q4: {myFund.CashAvailable}");
Console.WriteLine($"Price per share:{myFund.PricePerShare(Q4)}");
Console.WriteLine();
Console.ReadLine();


 
