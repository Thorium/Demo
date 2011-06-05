module SlideDemo

open System;

type PresentationKeeper(name: string, employer : string) = 
        member lollis.name = name
        member fuulis.employer = employer
        override this.ToString() = name + ", " + employer

let Rami = PresentationKeeper("Rami Karjalainen", "Reaktor")
let Tuomas = PresentationKeeper("Tuomas Hietanen", "Basware")

let topics = [ (2, "Functional programming from c# perspective, Linq principles", Tuomas);
               (1, "Functional programming in general", Rami);
               (3, "F# examples: ’basic’ stuff", Rami);
               (4, "F# and C# interoperability", Rami);
               (5, "F# vs. C#", Tuomas) ]

let formatTopic topic keeper = String.Format ("{0} ({1})", topic, keeper.ToString())

let pimpTheList = topics |> Seq.sortBy (fun (prio,_,_) -> prio) 
                         |> Seq.map (fun (_, a, b) -> a,b)
                         |> Seq.map (fun x -> (formatTopic (fst x) (snd x)))

let printPimped x = x |> Seq.iter (fun c -> printfn "%s" c)