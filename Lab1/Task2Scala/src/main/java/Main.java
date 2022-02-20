import SomeFunctions.SomeFunc;
import SomeFunctions.SimpleShapes;
import scala.collection.JavaConverters;
import java.util.ArrayList;

public class Main {
    public static void main(String[] args) {
        var list = new ArrayList<>();
        for (int i = 0; i <= 10000000; i++) list.add(i);

        System.out.printf(
                "Sum of big int list = %d\n",
                SomeFunc.listSum(JavaConverters.asScalaBuffer(list).toList(), 0));

        System.out.printf(
                "Area of rectangle with height = 5 and width = 6 is %f\n",
                SimpleShapes.Area(new SimpleShapes.Rectangle(5, 6)));

        System.out.printf(
                "Area of triangle with base = 5 and height = 6 is %f\n",
                SimpleShapes.Area(new SimpleShapes.Triangle(5, 6)));

        System.out.printf(
                "Area of circle with radius = 5 is %f\n",
                SimpleShapes.Area(new SimpleShapes.Circle(5)));

        System.out.println("\nAll pairs with int 0..7 with sum = 9");
        System.out.println(SomeFunc.getPairsWithSum(7, 9));

        int x = SomeFunc.double3TimesAndViewSteps(6);
        System.out.printf("\nFinal result x = %d", x);
    }
}
