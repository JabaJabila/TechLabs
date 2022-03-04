public class CppLibrary {
    static {
        System.loadLibrary("CppLibrary");
    }

    public native void hello();
    public native int sum(int a, int b);
    public native int diff(int a, int b);
}

// javac -h . ./src/CppLibrary.java
// g++ -c -I"%JAVA_HOME%\include" -I"%JAVA_HOME%\include\win32" CppLibrary.cpp -o CppLibrary.o
// g++ -shared -o CppLibrary.dll CppLibrary.o -W