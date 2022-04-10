using System.ComponentModel;

namespace TestProject;

public class Test
{
    private List<string> Property1 { get; }
    
    public BindingList<Test> Property2 { get; }
    
    public char[] Method1()
    {
        return new char[10];
    }
    
    public List<(Test, string)> Method2()
    {
        return new List<(Test, string)>();
    }

    public static void DoSomething(int n)
    {
    }

    public static void Main()
    {
        var s = "12314";
        
        if (s == null)
            DoSomething(1);

        if (s != null)
            DoSomething(2);
    }
}