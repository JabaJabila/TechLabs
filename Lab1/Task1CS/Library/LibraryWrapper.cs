using System.Runtime.InteropServices;

namespace Task1CS.Library
{
    internal class LibraryWrapper
    {
        [DllImport("CppLibrary.dll")]
        internal static extern void hello();

        [DllImport("CppLibrary.dll")]
        internal static extern int sum(int a, int b);

        [DllImport("CppLibrary.dll")]
        internal static extern int diff(int a, int b);
    }
}
