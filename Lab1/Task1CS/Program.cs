using Task1CS.Library;

namespace Task1CS;

internal class Program
{
    internal static void Main()
    {
        var lib = new CppLibrary();

        int a = 5;
        int b = 3;

        lib.Hello();
        Console.WriteLine($"{a} + {b} = {lib.Sum(a, b)}");
        Console.WriteLine($"{a} - {b} = {lib.Diff(a, b)}");
    }
}

