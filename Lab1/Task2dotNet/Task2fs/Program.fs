open SomeFunctions

let house = SomeFunctions.HouseBuilder.HouseBuilder()

printfn "%A" <| house { () }

printfn "%A" <| house{ 
                    floor SomeFunctions.HouseBuilder.SingleFloor
                    chimney
                    }