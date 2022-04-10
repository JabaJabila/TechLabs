using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;

namespace JabaJabilaAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ReturnIReadOnlyCollectionAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "JABA0003";
        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.JABA0003Title), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.JABA0003Format), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.JABA0003Description), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Usage";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSymbolAction(Analyze, SymbolKind.Property, SymbolKind.Method);
        }

        private static void Analyze(SymbolAnalysisContext context)
        {
            var listInterfaceIdentifier = @"^System.Collections.Generic.IList<.*>$";

            if (context.Symbol.Kind == SymbolKind.Property && (((IPropertySymbol)context.Symbol).DeclaredAccessibility == Accessibility.Public))
            {
                var propSymbol = (IPropertySymbol) context.Symbol;
                var typeSymbol = propSymbol.Type;
                var namedSymbol = typeSymbol is INamedTypeSymbol symbol ? symbol : null;
                if (namedSymbol is null)
                {
                    if (typeSymbol.TypeKind != TypeKind.Array) return;
                }
                else
                {
                    if (!typeSymbol.AllInterfaces.Any(i => Regex.IsMatch(i.ToDisplayString(), listInterfaceIdentifier))) return;
                }

                var diagnostic = Diagnostic.Create(Rule, propSymbol.Locations.First(), "public property " + propSymbol.ToString());
                context.ReportDiagnostic(diagnostic);
            }
            else if (context.Symbol.Kind == SymbolKind.Method && ((IMethodSymbol)context.Symbol).DeclaredAccessibility == Accessibility.Public)
            {
                var methodSymbol = (IMethodSymbol)context.Symbol;
                var typeSymbol = methodSymbol.ReturnType;
                var namedSymbol = typeSymbol is INamedTypeSymbol symbol ? symbol : null;
                if (namedSymbol is null)
                {
                    if (typeSymbol.TypeKind != TypeKind.Array) return;
                }
                else
                {
                    if (!typeSymbol.AllInterfaces.Any(i => Regex.IsMatch(i.ToDisplayString(), listInterfaceIdentifier))) return;
                }

                var diagnostic = Diagnostic.Create(Rule, methodSymbol.Locations.First(), "public method " + methodSymbol.ToString());
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
