module ClientLogics
open Microsoft.FSharp.Control.WebExtensions
open System
open System.Runtime.Serialization
open System.Runtime.Serialization.Json
open System.Net
open System.IO

[<DataContract>]
type StockQuote = { 
    [<DataMember>] mutable Date : DateTime
    [<DataMember>] mutable Rate : double  
    }

let byRate (s:StockQuote) = s.Rate

let DateMaxClose list = Seq.maxBy byRate list
let AvgClose list = Seq.averageBy byRate list

/// Object from Json 
let internal unjson<'t> (jsonString:string)  : 't =  
        use ms = new System.IO.MemoryStream(System.Text.Encoding.BigEndianUnicode.GetBytes(jsonString)) 
        let obj = (new DataContractJsonSerializer(typeof<'t>)).ReadObject(ms) 
        obj :?> 't
 
let mutable callresult = Seq.empty

let internal fetchAsync (url : Uri) trigger = 
    let req = HttpWebRequest.CreateHttp url
    req.CookieContainer <- new CookieContainer()
    let asynccall =
        async{
            try
                let! res = req.AsyncGetResponse() 
                use stream = res.GetResponseStream()
                use reader = new StreamReader(stream)
                let! rdata = reader.AsyncReadToEnd()                             
                callresult <- unjson<seq<StockQuote>> rdata
                trigger "" |> ignore
            with
                | _ as ex -> //for debug
                    failwith(ex.ToString()) 
        }

    asynccall |> Async.StartImmediate
    
