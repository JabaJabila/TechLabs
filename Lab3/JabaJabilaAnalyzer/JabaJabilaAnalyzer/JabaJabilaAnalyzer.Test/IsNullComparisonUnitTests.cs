using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;

using VerifyCS = JabaJabilaAnalyzer.Test.CSharpCodeFixVerifier<
    JabaJabilaAnalyzer.IsNullComparisonAnalyzer,
    JabaJabilaAnalyzer.IsNullComparisonAnalyzerCodeFixProvider>;

namespace JabaJabilaAnalyzer.Test
{
    [TestClass]
    public class IsNullComparisonUnitTests
    {
        [TestMethod]
        public async Task NoCode_NoDiagnostics()
        {
            var test = @"";

            await VerifyCS.VerifyAnalyzerAsync(test);
        }

        [TestMethod]
        public async Task NullCheckIsStatement_NoDiagnosticsNeeded()
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
                var s = string.Empty;
                if (s is null)
                    return true;
                return false;
            }
        }
    }";

            await VerifyCS.VerifyAnalyzerAsync(test);
        }

        [TestMethod]
        public async Task StringIsNullOrEmpty_NoDiagnosticsNeeded()
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
                var s = string.Empty;
                if (string.IsNullOrEmpty(s))
                    return true;
                return false;
            }
        }
    }";
            await VerifyCS.VerifyAnalyzerAsync(test);
        }

        [TestMethod]
        public async Task IfStatement_EqualsEqualsNullCheck()
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
                var s = string.Empty;
                if (s == null) 
                {
                    return true;
                }
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
                var s = string.Empty;
                if (s is null) 
                {
                    return true;
                }
                return false;
            }
        }
    }";

            var diagnosticResult = new DiagnosticResult("JABA0001", DiagnosticSeverity.Warning).WithSpan(16, 21, 16, 30);
            await VerifyCS.VerifyCodeFixAsync(test, diagnosticResult, fixtest);
        }

        [TestMethod]
        public async Task TernaryStatement_EqualsEqualsNullCheck()
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
                var s = string.Empty;
                return (s == null) ? true : false;
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
                var s = string.Empty;
                return (s is null) ? true : false;
            }
        }
    }";

            var diagnosticResult = new DiagnosticResult("JABA0001", DiagnosticSeverity.Warning).WithSpan(16, 25, 16, 34);
            await VerifyCS.VerifyCodeFixAsync(test, diagnosticResult, fixtest);
        }
    }
}
