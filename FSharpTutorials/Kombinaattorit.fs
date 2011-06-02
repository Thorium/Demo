module Kombinaattorit

//Sulutus ei ole pakollista:
let f(x,y) = x+y
let g x y  = x+y
 
//Funktion tyyppi:
let I x = x
//     x: a
//     I: a -> a


let K x y = x
//       x: a
//       y: b
//       K: a -> b -> c


let S x y z = x(z)(y(z))
//        x:  f(a  b)
//        z:  a
//        x:  a -> b -> c
//        y:  a -> b
//  S x y z = (a -> b -> c) -> (a -> b) -> a -> c



//Func<Func<a, b, c>, Func<a, b>, a, c> S = (x, y, z) => x(z,y(z));

// Osittainen suoritus (Currying)
let vakioMoi = K "Moi"
let moi = vakioMoi 123




//function composition
let yhdistä1 f g x = g(f x)
let yhdistä2 f g x = (f >> g) x
 
//val yhdistä1 : ('a -> 'b) -> ('b -> 'c) -> 'a -> 'c
//val yhdistä2 : ('a -> 'b) -> ('b -> 'c) -> 'a -> 'c


let yhdistä3 f g = (f >> g)
 

//käytännön esimerkki:
//let käsittele = (tallenna >> validoi >> lähetä)

 
