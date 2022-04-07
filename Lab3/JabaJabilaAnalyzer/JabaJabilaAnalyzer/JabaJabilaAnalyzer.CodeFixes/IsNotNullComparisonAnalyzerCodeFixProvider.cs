using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace JabaJabilaAnalyzer
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(IsNotNullComparisonAnalyzerCodeFixProvider))]
    [Shared]
    public class IsNotNullComparisonAnalyzerCodeFixProvider : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds =>
            ImmutableArray.Create(IsNotNullComparisonAnalyzer.DiagnosticId);

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;
            var declaration = root.FindNode(diagnosticSpan).AncestorsAndSelf().OfType<BinaryExpressionSyntax>().First();

            context.RegisterCodeFix(
                CodeAction.Create(
                    CodeFixResources.JABA0002Fix,
                    c => MakeIsNotNull(context.Document, declaration),
                    nameof(CodeFixResources.JABA0002Fix)),
                diagnostic);
        }

        private async Task<Solution> MakeIsNotNull(Document document, BinaryExpressionSyntax binExpr)
        {
            var right = binExpr.Right;
            var left = binExpr.Left;
            ExpressionSyntax nullExpr, notNullExpr;
            (nullExpr, notNullExpr) = right.IsKind(SyntaxKind.NullLiteralExpression) ? (right, left) : (left, right);

            if (!document.TryGetSyntaxRoot(out var root)) return document.Project.Solution;
            var editor = new SyntaxEditor(root, document.Project.Solution.Workspace);
            var isExpr = SyntaxFactory.IsPatternExpression(
                notNullExpr,
                SyntaxFactory.UnaryPattern(SyntaxFactory.Token(SyntaxKind.NotKeyword),
                    SyntaxFactory.ConstantPattern(nullExpr)));

            editor.ReplaceNode(binExpr, isExpr);

            return document.WithSyntaxRoot(editor.GetChangedRoot()).Project.Solution;
        }
    }
}