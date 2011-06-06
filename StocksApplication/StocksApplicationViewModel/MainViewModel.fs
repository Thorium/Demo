#light
namespace StocksViewModel

open System
open System.ComponentModel

type MainViewModel() =

    let mutable fromDate = DateTime.Now.AddDays(-1.0).AddMonths(-2)
    let mutable toDate = DateTime.Now.AddDays(-1.0)
    let mutable symbol = "MSFT"
    let mutable rates = Seq.empty<ClientLogics.StockQuote>
    let mutable maxDate = DateTime.Now
    let mutable maxRate = 0.00
    let mutable avgClose = 0.00
    
    //Silverlight databinding interface
    let event = new Event<_,_>()
    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member x.PropertyChanged = event.Publish
    
    member x.TriggerPropertyChanged(name)=
        event.Trigger(x, new PropertyChangedEventArgs(name))
    
    member x.FromDate 
        with get() = fromDate
        and set t = 
                fromDate <- t
                x.TriggerPropertyChanged "FromDate"

    member x.ToDate 
        with get() = toDate
        and set t = 
                toDate <- t
                x.TriggerPropertyChanged "ToDate"

    member x.Symbol
        with get() = symbol
        and set t = 
                symbol <- t
                x.TriggerPropertyChanged "Symbol"

    member x.Rates
        with get() = rates
        and set t = 
                rates <- t
                let trigger p = x.TriggerPropertyChanged p
                maxRate <- ClientLogics.DateMaxClose(rates).Rate
                maxDate <- ClientLogics.DateMaxClose(rates).Date
                avgClose <- ClientLogics.AvgClose(rates)
                List.iter trigger ["Rates";"MaxDate";"MaxValue";"AvgClose"] |> ignore

    member x.UpdateData (_:Object) (_:EventArgs) =
        //Service path
        let server = "http://localhost:49624/StocksService.svc"
        let service = new Uri(server + "/Symbol/" + x.Symbol + "/" + 
                                x.FromDate.ToString("yyyyMMdd") + "/" +
                                x.ToDate.ToString("yyyyMMdd"), UriKind.Absolute)
        //UI-thread syncronization
        let trigger _ = 
            let update _ = x.Rates <- ClientLogics.callresult
            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(new Action(update)) |> ignore
        ClientLogics.fetchAsync service trigger

    member x.MaxDate = maxDate
    member x.MaxValue = maxRate
    member x.AvgClose = avgClose
