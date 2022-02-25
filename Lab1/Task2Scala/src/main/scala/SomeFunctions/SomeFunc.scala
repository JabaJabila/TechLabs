import scala.annotation.tailrec
import scala.util.chaining.scalaUtilChainingOps

package SomeFunctions {
  object SomeFunc {
    @tailrec
    final def listSum(list: List[Int], cur_sum: Long = 0L):
    Long = {
      if (list.isEmpty) cur_sum
      else listSum(list.tail, cur_sum + list.head)
    }

    def getPairsWithSum(maxInt: Int, requiredSum: Int) =
      for (i <- 0 until maxInt + 1;
           j <- 0 until maxInt + 1 if (i + j == requiredSum))
        yield(i, j)

    private def double(x: Int) = x * 2

    def double3TimesAndViewSteps(x : Int): Int = {
      println(s"Start doubling x = $x")
      x.pipe(double)
        .tap(res => println(s"After 1 double: $res"))
        .pipe(double)
        .tap(res => println(s"After 2 double: $res"))
        .pipe(double)
        .tap(res => println(s"After 3 double: $res"))
    }
  }

  object SimpleShapes {
    trait Shape
    case class Rectangle(Height : Double, Width : Double) extends Shape
    case class Triangle(Base: Double, Height : Double) extends Shape
    case class Circle(Radius : Double) extends Shape

    def Area(shape: Shape): Double = {
      shape match {
        case Rectangle(h, w) => h * w
        case Triangle(b, h) => b * h / 2.0
        case Circle(r) => 3.14 * r * r
      }
    }
  }
}