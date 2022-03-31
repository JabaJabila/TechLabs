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
    public class IsNotNullComparisonAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "JABA0002";
        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.JABA0002Title), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.JABA0002Format), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.JABA0002Description), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Comparison";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(Analyze, SyntaxKind.NotEqualsExpression);
        }

        private static void Analyze(SyntaxNodeAnalysisContext context)
        {
            var node = context.Node;
            if (!(node is BinaryExpressionSyntax notEqualsNode &&
                notEqualsNode.OperatorToken.IsKind(SyntaxKind.ExclamationEqualsToken) &&
                notEqualsNode.Right.IsKind(SyntaxKind.NullLiteralExpression))) return;

            var diagnostic = Diagnostic.Create(Rule, node.GetLocation(), node.ToString());
            context.ReportDiagnostic(diagnostic);
        }
    }
}
