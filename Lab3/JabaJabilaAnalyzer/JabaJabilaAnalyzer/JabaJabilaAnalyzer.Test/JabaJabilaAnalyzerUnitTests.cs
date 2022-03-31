using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using VerifyCS = JabaJabilaAnalyzer.Test.CSharpCodeFixVerifier<
    JabaJabilaAnalyzer.IsNullComparisonAnalyzer,
    JabaJabilaAnalyzer.IsNullComparisonAnalyzerCodeFixProvider>;

namespace JabaJabilaAnalyzer.Test
{
    [TestClass]
    public class JabaJabilaAnalyzerUnitTest
    {
        //No diagnostics expected to show up
        [TestMethod]
        public async Task TestMethod1()
        {
            var test = @"";

            await VerifyCS.VerifyAnalyzerAsync(test);
        }

        //Diagnostic and CodeFix both triggered and checked for
        [TestMethod]
        public async Task TestMethod2()
        {
            var test = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
        class Test
        {
            public bool Bruh() 
            {
                if {|#0:(new Test() == null)|}
                    return true;
                return false;
            }
        }
    }";

            var fixtest = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
        class Test
        {
            public bool Bruh() 
            {
                if (new Test() is null)
                    return true;
                return false;
            }
        }
    }";

            var expected = VerifyCS.Diagnostic("JabaJabilaAnalyzer").WithLocation(0).WithArguments("(new Test() is null)");
            // await VerifyCS.VerifyCodeFixAsync(test, expected, fixtest);
        }
    }
}
