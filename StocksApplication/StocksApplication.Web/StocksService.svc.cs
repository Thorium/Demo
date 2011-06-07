using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Linq;

namespace StocksApplication.Web
{
    [ServiceContract(Name = "StocksService", Namespace = "http://localhost/StocksService/", SessionMode = SessionMode.NotAllowed)]
    [ServiceKnownType(typeof(string)), ServiceKnownType(typeof(IEnumerable<Stocks.StockQuote>))]
    [ServiceBehavior(Name = "StocksService", Namespace = "http://localhost/StocksService/")]
    public class StocksService
    {
        /// <summary> Get quotes </summary>
        [OperationContract(Name = "FetchStockData")]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "Symbol/{quote}/{from}/{to}")]
        public IEnumerable<Stocks.StockQuote> FetchStockData(string quote, string from, string to)
        {
            var fromdate = DateTime.ParseExact(from, "yyyyMMdd", CultureInfo.InvariantCulture);
            var todate = DateTime.ParseExact(to, "yyyyMMdd", CultureInfo.InvariantCulture);
            Contract.Assert(fromdate <= todate, "Fromdate must be before todate");
            Contract.Assert(todate <= DateTime.Now, "Future not allowed yet...");

            var uri = Stocks.MakeUrl(quote, fromdate, todate);

            try { 
                return Stocks.GetResult(uri);
            }catch(System.Net.WebException){
                //throw;
                return Enumerable.Repeat(new Stocks.StockQuote(todate, -1.0), 1);
            }
        }
        // example: http://localhost:49624/StocksService.svc/Symbol/MSFT/20100905/20100910
    }
}
