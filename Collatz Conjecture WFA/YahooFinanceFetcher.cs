using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YahooFinanceApi;

namespace Collatz_Conjecture_WFA
{
    public class YahooFinanceFetcher
    {
        // Function to fetch stock data from Yahoo API
        public async Task<List<double>> GetStockPricesAsync(string[] symbols, string field)
        {
            try
            {
                // Query Yahoo Finance API with specified symbols
                var securities = await Yahoo.Symbols(symbols)
                    .Fields(field)
                    .QueryAsync();

                // Extract the specified field's data (e.g., regularMarketPrice)
                var stockPrices = new List<double>();
                foreach (var security in securities.Values)
                {
                    if (security.Fields.ContainsKey(field))
                    {
                        stockPrices.Add((double)security.Fields[field]);
                    }
                }

                return stockPrices;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data from Yahoo Finance API: {ex.Message}");
                return null;
            }
        }
    }
}

