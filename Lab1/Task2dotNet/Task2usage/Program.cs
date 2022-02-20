using SomeFunctions;

namespace Task2usage;

public class Program
{
    public static void Main()
    {
        ListOperations.PrintSquaredEven(1, 100);
        ListOperations.PrintTripledOdd(1, 100);

        var triangle = new SimpleShapes.Triangle(5, 6);
        var rectangle = new SimpleShapes.Rectangle(4, 6);
        var circle = new SimpleShapes.Circle(7);

        Console.WriteLine();
        Console.WriteLine(SimpleShapes.Area(SimpleShapes.Shape.NewTriangle(triangle)));
        Console.WriteLine(SimpleShapes.Area(SimpleShapes.Shape.NewRectangle(rectangle)));
        Console.WriteLine(SimpleShapes.Area(SimpleShapes.Shape.NewCircle(circle)));

        Console.WriteLine();
        var house = new HouseBuilder.HouseBuilder();
        var baseHouse = HouseBuilder.baseHouse;
        Console.WriteLine(HouseBuilder.baseHouse);

        // House with single floor and chimney
        Console.WriteLine(house.Floor(house.Chimney(baseHouse), HouseBuilder.Floor.SingleFloor));

        // Stone house with single floor and chimney
        Console.WriteLine(
            house.Floor(
                house.Material(house.Chimney(baseHouse), HouseBuilder.Material.Stone),
                HouseBuilder.Floor.SingleFloor));
    }
}