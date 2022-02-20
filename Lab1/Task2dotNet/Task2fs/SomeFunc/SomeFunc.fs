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

module HouseBuilder =
    type Floor =
        | SingleFloor
        | DoubleFloor
        | TripleFloor

    type Material =
        | Wood
        | Stone
        | Brick

    type Roof =
        | Wood
        | Metal
        | Tile

    type House =
        { Floor : Floor
          Material : Material
          Roof : Roof
          Chimney : bool }

    let baseHouse = 
        { Floor = DoubleFloor
          Material = Brick
          Roof = Tile
          Chimney = false }

    type HouseBuilder() =
        member _.Zero _ = baseHouse
        member _.Yield _ = baseHouse

        [<CustomOperation("floor")>]
        member _.Floor(house, floor) = { house with Floor = floor } 

        [<CustomOperation("material")>]
        member _.Material(house, material) = { house with Material = material } 

        [<CustomOperation("roof")>]
        member _.Roof(house, roof) = { house with Roof = roof }
        
        [<CustomOperation("chimney")>]
        member _.Chimney(house) = { house with Chimney = true }