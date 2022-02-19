public class Main
{
    public static void main(String[] args)
    {
        var lib = new CppLibrary();

        int a = 5;
        int b = 3;

        lib.hello();
        System.out.printf("%d + %d = %d\n", a, b, lib.sum(a, b));
        System.out.printf("%d - %d = %d\n", a, b, lib.diff(a, b));
    }
}
