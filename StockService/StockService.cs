using CsvHelper;
using FinancialChat.Abstractions.StockService;
using FinancialChat.StockService.Models;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace FinancialChat.StockService
{
    public class StockService : IStockService
    {
        private readonly HttpClient _httpClient;

        public StockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetStockQuote(string stockCode)
        {
            string result = null;
            string responseContent;
            try
            {
                var stooqResponse = await _httpClient.GetAsync($"q/l/?s={ stockCode }&f=sd2t2ohlcv&h&e=csv​");
                stooqResponse.EnsureSuccessStatusCode();
                responseContent = stooqResponse.Content.ReadAsStringAsync().Result;
            }
            catch (System.Exception)
            {
                // Should Log exception accordingly
                return "Couldn't connect to Stock data server";
            }

            using (TextReader textReader = new StringReader(responseContent))
            using (var reader = new CsvReader(textReader))
            {
                try
                {
                    if (!reader.Read())
                    {
                        result = $"Couldn't find any stock data for '{ stockCode }'";
                    }

                    var stooqItemRecord = reader.GetRecord<StooqItem>();
                    if (stooqItemRecord.Open.ToLower() != "n/d"
                        && decimal.TryParse(stooqItemRecord.Open, out decimal openAmount))
                    {
                        result = $"'{ stooqItemRecord.Symbol }' quote is { openAmount.ToString("C") } per share";
                    }
                    else
                    {
                        result = $"There isn't any available data for '{ stooqItemRecord.Symbol }' quote";
                    }
                }
                catch (System.Exception)
                {
                    // should log problem here
                    result = $"An internal issue occurred trying to fetch stock quote. Please contact your Administrator.";
                }
            }
            return result;
        }
    }
}
