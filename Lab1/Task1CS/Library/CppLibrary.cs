namespace Task1CS.Library
{
    public class CppLibrary
    {
        public void Hello()
        {
            LibraryWrapper.hello();
        }

        public int Sum(int a, int b)
        {
            return LibraryWrapper.sum(a, b);
        }

        public int Diff(int a, int b)
        {
            return LibraryWrapper.diff(a, b);
        }
    }
}
