using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace JabaJabilaAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class IsNullComparisonAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "JABA0001";
        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.JABA0001Title), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.JABA0001Format), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.JABA0001Description), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Usage";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(Analyze,SyntaxKind.EqualsExpression);
        }

        private static void Analyze(SyntaxNodeAnalysisContext context)
        {
            var node = context.Node;
            if (!(node is BinaryExpressionSyntax equalsNode &&
                equalsNode.OperatorToken.IsKind(SyntaxKind.EqualsEqualsToken) &&
                (equalsNode.Right.IsKind(SyntaxKind.NullLiteralExpression) || equalsNode.Left.IsKind(SyntaxKind.NullLiteralExpression))))
                return;
            
            var diagnostic = Diagnostic.Create(Rule, node.GetLocation(), node.ToString());
            context.ReportDiagnostic(diagnostic);
        }
    }
}
