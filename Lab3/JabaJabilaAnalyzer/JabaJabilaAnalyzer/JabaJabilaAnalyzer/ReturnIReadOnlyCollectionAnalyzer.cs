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
        private const string ListInterfaceIdentifier = @"^System.Collections.Generic.IList<.*>$";

        public const string DiagnosticId = "JABA0003";
        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.JABA0003Title), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.JABA0003Format), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.JABA0003Description), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Usage";

        private static readonly DiagnosticDescriptor Rule = 
            new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSymbolAction(Analyze, SymbolKind.Property, SymbolKind.Method);
        }

        private static void Analyze(SymbolAnalysisContext context)
        {
            if (context.Symbol.Kind == SymbolKind.Property)
            {
                var propSymbol = (IPropertySymbol) context.Symbol;
                if (propSymbol.DeclaredAccessibility != Accessibility.Public) return;

                var typeSymbol = propSymbol.Type;
                if (!CheckIfArrayOrList(typeSymbol)) return;
                var diagnostic = Diagnostic.Create(Rule, propSymbol.Locations.First(), "public property " + propSymbol.ToString());
                context.ReportDiagnostic(diagnostic);
            }
            else if (context.Symbol.Kind == SymbolKind.Method)
            {
                var methodSymbol = (IMethodSymbol)context.Symbol;
                if (methodSymbol.DeclaredAccessibility != Accessibility.Public) return;

                var typeSymbol = methodSymbol.ReturnType;
                if (!CheckIfArrayOrList(typeSymbol)) return;
                var diagnostic = Diagnostic.Create(Rule, methodSymbol.Locations.First(), "public method " + methodSymbol.ToString());
                context.ReportDiagnostic(diagnostic);
            }
        }

        private static bool CheckIfArrayOrList(ITypeSymbol typeSymbol) 
        {
            var namedSymbol = typeSymbol is INamedTypeSymbol symbol ? symbol : null;

            if (namedSymbol is null)
            {
                if (typeSymbol.TypeKind != TypeKind.Array) return false;
            }
            else
            {
                if (!typeSymbol.AllInterfaces.Any(i => 
                        Regex.IsMatch(i.ToDisplayString(), ListInterfaceIdentifier))) return false;
            }

            return true;
        }
    }
}
