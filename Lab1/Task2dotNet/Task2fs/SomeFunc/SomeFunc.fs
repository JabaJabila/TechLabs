namespace SomeFunctions

module SimpleShapes =
    type Rectangle = {Width : double; Height : double}
    type Triangle = {Base : double; Height : double}
    type Circle = {Radius : double}

    type Shape =
        | Rectangle of Rectangle
        | Triangle of Triangle
        | Circle of Circle

    let Area (shape : Shape) =
        match shape with
        | Rectangle {Width = w; Height = h} -> w * h
        | Triangle {Base = w; Height = h} -> w * h / 2.
        | Circle {Radius = r} -> r ** 2. * 3.14

module ListOperations =
    let SquaredEven (start : int) (stop : int) : list<float> = 
        [start .. stop]
        |> List.where(fun c -> (c % 2) = 0)
        |> List.map(fun c -> (float c) ** 2.)

    let TripledOdd (start : int) (stop : int) : list<float> = 
        let l = [start .. stop]
        query {
            for i in l do
            where (i % 2 = 1)
            select ((float i) ** 3.)
        } |> Seq.toList
        
    let PrintSquaredEven (start : int) (stop : int) = printf "%A\n" <| SquaredEven start stop
    let PrintTripledOdd (start : int) (stop : int) = printf "%A\n" <| TripledOdd start stop