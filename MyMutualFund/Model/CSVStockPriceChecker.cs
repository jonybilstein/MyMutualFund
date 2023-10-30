﻿using MyMutualFund.Interfaces;
using MyMutualFund.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMutualFind.Model
{
    public class CSVStockPriceChecker : IStockPriceChecker
    {

        private List<StockPrice> StockPrices = new List<StockPrice>();
        public StockPrice? GetPrice(string symbol, DateTime date)
        {
            var stockPriceObj = StockPrices.Where(x => x.TickerSymbol == symbol).OrderByDescending(x => x.Date).FirstOrDefault();
            return stockPriceObj;
        }

        public bool LoadCSV(string path)
        {
            using var reader = new StreamReader(path);
            
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                var price = new StockPrice()
                {
                    TickerSymbol = values[0],
                    Date = DateTime.Parse(values[1]),
                    Price = decimal.Parse(values[2])
                };
                StockPrices.Add(price);
            }
            
            return true;
        }
    }
}