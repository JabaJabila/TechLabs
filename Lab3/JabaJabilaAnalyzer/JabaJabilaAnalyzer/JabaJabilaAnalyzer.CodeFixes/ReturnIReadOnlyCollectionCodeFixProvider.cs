using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace JabaJabilaAnalyzer
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(ReturnIReadOnlyCollectionCodeFixProvider))]
    [Shared]
    public class ReturnIReadOnlyCollectionCodeFixProvider : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds =>
            ImmutableArray.Create(ReturnIReadOnlyCollectionAnalyzer.DiagnosticId);

        public string[] Ress { get; }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;
            var declaration = root.FindNode(diagnosticSpan);

            context.RegisterCodeFix(
                CodeAction.Create(
                    CodeFixResources.JABA0003Fix,
                    c => MakeReturnIReadOnlyCollection(context.Document, declaration, c),
                    nameof(CodeFixResources.JABA0003Fix)),
                diagnostic);
        }

        private async Task<Solution> MakeReturnIReadOnlyCollection(Document document, SyntaxNode node, CancellationToken cancellationToken)
        {
            var semantic = await document.GetSemanticModelAsync(cancellationToken);
            var callNode = node.AncestorsAndSelf().First(n => n.IsKind(SyntaxKind.PropertyDeclaration) || n.IsKind(SyntaxKind.MethodDeclaration));
            var collectionIdentifier = SyntaxFactory.Identifier("IReadOnlyCollection");
            if (!document.TryGetSyntaxRoot(out var root)) return document.Project.Solution;
            var editor = new SyntaxEditor(root, document.Project.Solution.Workspace);

            if (callNode.IsKind(SyntaxKind.PropertyDeclaration))
            {
                var propNode = (PropertyDeclarationSyntax) callNode;
                var type = propNode.Type;
                if (type.IsKind(SyntaxKind.ArrayType))
                {
                    var arrayType = (ArrayTypeSyntax) type;
                    var elementType = arrayType.ElementType;

                    var newNode = SyntaxFactory.GenericName(
                        collectionIdentifier,
                        SyntaxFactory.TypeArgumentList(SyntaxFactory.SeparatedList(new List<TypeSyntax> {elementType})));

                    editor.ReplaceNode(arrayType, newNode);
                }

                else if (type.IsKind(SyntaxKind.GenericName))
                {
                    var genericName = (GenericNameSyntax) type;
                    var elementTypes = genericName.TypeArgumentList;
                    var newNode = SyntaxFactory.GenericName(collectionIdentifier, elementTypes);

                    editor.ReplaceNode(genericName, newNode);
                }
            }

            else if (callNode.IsKind(SyntaxKind.MethodDeclaration))
            {
                var methodNode = (MethodDeclarationSyntax)callNode;
                var type = methodNode.ReturnType;

                if (type.IsKind(SyntaxKind.ArrayType))
                {
                    var arrayType = (ArrayTypeSyntax)type;
                    var elementType = arrayType.ElementType;

                    var newNode = SyntaxFactory.GenericName(
                        collectionIdentifier,
                        SyntaxFactory.TypeArgumentList(SyntaxFactory.SeparatedList(new List<TypeSyntax> { elementType })));

                    editor.ReplaceNode(arrayType, newNode);
                }

                else if (type.IsKind(SyntaxKind.GenericName))
                {
                    var genericName = (GenericNameSyntax)type;
                    var elementTypes = genericName.TypeArgumentList;
                    var newNode = SyntaxFactory.GenericName(collectionIdentifier, elementTypes);

                    editor.ReplaceNode(genericName, newNode);
                }
            }

            return document.WithSyntaxRoot(editor.GetChangedRoot()).Project.Solution;
        }
    }
}