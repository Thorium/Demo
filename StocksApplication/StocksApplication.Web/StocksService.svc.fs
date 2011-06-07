//Service wcf F# could be used. However this is pure class structure definition, so no big win here
//#r "System.ServiceModel"
//#r "System.ServiceModel.Web"
namespace StocksApplication.Web

open System
open System.Collections.Generic
open System.Diagnostics.Contracts
open System.Globalization
open System.Linq
open System.ServiceModel
open System.ServiceModel.Web
open Stocks

[<ServiceContract(Name = "StocksService", Namespace = "http://localhost/StocksService/", SessionMode = SessionMode.NotAllowed)>]
[<ServiceBehavior(Name = "StocksService", Namespace = "http://localhost/StocksService/")>]
type StocksService =

    /// <summary> Get quotes </summary>
    [<OperationContract(Name = "FetchStockData")>]
    [<WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "Symbol/{quote}/{from}/{to}")>]
    member x. FetchStockData (quote:string) (fromd:string) (tod:string) =

        let formatdate d = DateTime.ParseExact(d, "yyyyMMdd", CultureInfo.InvariantCulture)
        let fromdate = formatdate fromd
        let todate = formatdate tod
        Contract.Assert(fromdate <= todate, "Fromdate must be before todate")
        Contract.Assert(todate <= DateTime.Now, "Future not allowed yet...")

        let uri = MakeUrl quote fromdate todate

        try
            GetResult uri
        with
            | _ as ex -> //for debug
                //failwith("GetResult returned error");
                let tmp = { Date = todate; Rate = -1.0 }
                List.toSeq [tmp]
       
    // example: http://localhost:49624/StocksService.svc/Symbol/MSFT/20100905/20100910

