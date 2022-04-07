using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;
using VerifyCS = JabaJabilaAnalyzer.Test.CSharpCodeFixVerifier<
    JabaJabilaAnalyzer.IsNotNullComparisonAnalyzer,
    JabaJabilaAnalyzer.IsNotNullComparisonAnalyzerCodeFixProvider>;

namespace JabaJabilaAnalyzer.Test
{
    [TestClass]
    public class JabaJabilaAnalyzerUnitTest
    {
        //No diagnostics expected to show up
        [TestMethod]
        public async Task NoCode_NoDiagnostics()
        {
            var test = @"";

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

        //Diagnostic and CodeFix both triggered and checked for
        [TestMethod]
        public async Task IfStatement_NotEqualsNullCheck()
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
                if (s != null)
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
                var s = string.Empty;
                if (s is not null)
                    return true;
                return false;
            }
        }
    }";
            // /0/Test0.cs(16,21): warning JABA0001: Expression 's == null' checks on null with '==' operator
            // VerifyCS.Diagnostic().WithSpan(16, 21, 16, 30).WithArguments("s == null")
            var diagnosticResult = new DiagnosticResult("JABA0002", DiagnosticSeverity.Warning).WithSpan(16, 21, 16, 30);
            await VerifyCS.VerifyCodeFixAsync(test, diagnosticResult, fixtest);
        }
    }
}
