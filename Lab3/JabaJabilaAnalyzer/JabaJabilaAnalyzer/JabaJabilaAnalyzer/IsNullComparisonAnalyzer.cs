using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

namespace JabaJabilaAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class IsNullComparisonAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "JabaJabilaAnalyzer";
        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Comparison";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(action =>
            {
                if (action.Node != null) return;
                if (!(action.Node is BinaryExpressionSyntax equalsNode)) return;
                if (!(equalsNode.OperatorToken.IsKind(SyntaxKind.EqualsEqualsToken) && equalsNode.Right.IsKind(SyntaxKind.NullLiteralExpression))) return;

                var diagnostic = Diagnostic.Create(Rule, action.Node.GetLocation());
                action.ReportDiagnostic(diagnostic);
            }, SyntaxKind.EqualsExpression);
        }
    }
}
