using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;
using VerifyCS = JabaJabilaAnalyzer.Test.CSharpCodeFixVerifier<
    JabaJabilaAnalyzer.ReturnIReadOnlyCollectionAnalyzer,
    JabaJabilaAnalyzer.ReturnIReadOnlyCollectionCodeFixProvider>;

namespace JabaJabilaAnalyzer.Test
{
    [TestClass]
    public class ReturnIReadOnlyCollectionUnitTests
    {
        //No diagnostics expected to show up
        [TestMethod]
        public async Task NoCode_NoDiagnostics()
        {
            var test = @"";

            await VerifyCS.VerifyAnalyzerAsync(test);
        }

        [TestMethod]
        public async Task IReadOnlyCollectionReturn_NoDiagnosticsNeeded()
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
            public IReadOnlyCollection<int> ints { get; set; }
        }
    }";

            await VerifyCS.VerifyAnalyzerAsync(test);
        }

        //Diagnostic and CodeFix both triggered and checked for
        [TestMethod]
        public async Task PropertyList()
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
            public List<int> ints { get; set; }
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
            public IReadOnlyCollection<int> ints { get; set; }
        }
    }";
            var diagnosticResult = new DiagnosticResult("JABA0003", DiagnosticSeverity.Warning).WithSpan(13, 30, 13, 34);
            var diagnosticResult2 = new DiagnosticResult("JABA0003", DiagnosticSeverity.Warning).WithSpan(13, 37, 13, 40);
            // await VerifyCS.VerifyAnalyzerAsync(test, diagnosticResult, diagnosticResult2);
            await VerifyCS.VerifyCodeFixAsync(test, new DiagnosticResult[] {diagnosticResult, diagnosticResult2}, fixtest);
        }

        //Diagnostic and CodeFix both triggered and checked for
        [TestMethod]
        public async Task PropertyArray()
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
            public int[] ints { get; set; }
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
            public IReadOnlyCollection<int> ints { get; set; }
        }
    }";

            var diagnosticResult = new DiagnosticResult("JABA0003", DiagnosticSeverity.Warning).WithSpan(13, 26, 13, 30);
            var diagnosticResult2 = new DiagnosticResult("JABA0003", DiagnosticSeverity.Warning).WithSpan(13, 33, 13, 36);
            // await VerifyCS.VerifyAnalyzerAsync(test, diagnosticResult, diagnosticResult2);
            await VerifyCS.VerifyCodeFixAsync(test, new DiagnosticResult[] {diagnosticResult, diagnosticResult2}, fixtest);
        }

        //Diagnostic and CodeFix both triggered and checked for
        [TestMethod]
        public async Task MethodPrivateArray()
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
            private int[] GetArray()
            {
                return new int[5];
            }
        }
    }";

            await VerifyCS.VerifyAnalyzerAsync(test);
        }

        //Diagnostic and CodeFix both triggered and checked for
        [TestMethod]
        public async Task MethodArray()
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
            public int[] GetArray()
            {
                return new int[5];
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
            public IReadOnlyCollection<int> GetArray()
            {
                return new int[5];
            }
        }
    }";

            var diagnosticResult = new DiagnosticResult("JABA0003", DiagnosticSeverity.Warning).WithSpan(13, 26, 13, 34);
            await VerifyCS.VerifyCodeFixAsync(test, diagnosticResult, fixtest);
        }

        //Diagnostic and CodeFix both triggered and checked for
        [TestMethod]
        public async Task MethodList()
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
            public List<string> GetList()
            {
                return new List<string>();
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
            public IReadOnlyCollection<string> GetList()
            {
                return new List<string>();
            }
        }
    }";

            var diagnosticResult = new DiagnosticResult("JABA0003", DiagnosticSeverity.Warning).WithSpan(13, 33, 13, 40);
            await VerifyCS.VerifyCodeFixAsync(test, diagnosticResult, fixtest);
        }
    }
}
