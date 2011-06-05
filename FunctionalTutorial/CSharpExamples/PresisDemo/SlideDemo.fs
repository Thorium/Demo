module SlideDemo

open System;

type PresentationKeeper(name: string, employer : string) = 
        member lollis.name = name
        member fuulis.employer = employer
        override this.ToString() = name + ", " + employer

let Rami = PresentationKeeper("Rami Karjalainen", "Reaktor")
let Tuomas = PresentationKeeper("Tuomas Hietanen", "Basware")

let topics = [ (2, "Funktionaalinen ohjelmointi C#-näkökulmasta, LINQ:n toimintaperiaatteet", Tuomas);
               (1, "Funktionaalisen ohjelmoinnin yleiskatsaus", Rami);
               (3, "F#:n käyttö yhdessä C#:n kanssa", Rami);
               (4, "Pari F#-esimerkkiä, peruskoodausta", Rami);
               (5, "F# vs C# esimerkki", Tuomas) ]

let formatTopic topic keeper = String.Format ("{0} ({1})", topic, keeper.ToString())

let pimpTheList = topics |> Seq.sortBy (fun (prio,_,_) -> prio) 
                         |> Seq.map (fun (_, a, b) -> a,b)
                         |> Seq.map (fun x -> (formatTopic (fst x) (snd x)))

let printPimped x = x |> Seq.iter (fun c -> printfn "%s" c)