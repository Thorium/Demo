module Stocks
open System
open System.Collections.Generic
open System.Globalization
open System.IO
open System.Net
let public MakeUrl symbol (dfrom:DateTime) (dto:DateTime) = 
            new Uri("http://ichart.finance.yahoo.com/table.csv?s=" + symbol +
               "&e=" + dto.Day.ToString() + "&d=" + dto.Month.ToString() + "&f=" + dto.Year.ToString() +
               "&g=d&b=" + dfrom.Day.ToString() + "&a=" + dfrom.Month.ToString() + "&c=" + dfrom.Year.ToString() +
               "&ignore=.csv")

let internal fetch (url : Uri) = 
    let req = WebRequest.Create (url) :?> HttpWebRequest
    use stream = req.GetResponse().GetResponseStream()
    use reader = new StreamReader(stream)
    reader.ReadToEnd()

let internal decompose (response:string) = 
    let split (mark:char) (data:string) =
        data.Split(mark) |> Array.toList
    response |> split '\n'
    |> List.filter (fun f -> f<>"")
    |> List.map (split ',') 


let internal reformat (sel) =
    let parseDate d = DateTime.ParseExact(d, "yyyy-mm-dd", CultureInfo.InvariantCulture)
    let parseRate r = Double.Parse(r, CultureInfo.GetCultureInfo("en-US"))
    let focus (l:string list) = 
        (parseDate l.[0], parseRate l.[4])
    Seq.skip 1 sel |> Seq.map focus

let public GetResult url = (fetch >> decompose >> reformat) url

let DateMaxClose list = Seq.maxBy snd list

//let req = MakeUrl "MSFT" (new DateTime(2010, 2, 20)) (new DateTime(2010, 3, 25)) 
//let AvgClose = (GetResult >> Seq.map snd >> Seq.average) req
