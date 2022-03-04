import SomeFunctions.SimpleShapes.{Area, Circle, Rectangle, Triangle}
import SomeFunctions.SomeFunc.{listSum, getPairsWithSum, double3TimesAndViewSteps}

object test {
    def main(args: Array[String]): Unit = {
      val list = (1 to 10000000).toList
      println(listSum(list))
      println(Area(Rectangle(5, 6)))
      println(Area(Triangle(5, 6)))
      println(Area(Circle(5)))
      print(getPairsWithSum(8, 8))
      double3TimesAndViewSteps(5)
    }
}