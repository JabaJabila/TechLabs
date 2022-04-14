using JavaParser;
using JavaParser.SemanticDataModels;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Codegen.ClientGenerators;

public class ClientGenerator : IClientGenerator
{
    private readonly IJavaCodeParser _parser;
    private readonly IRequestMethodGenerator _methodGenerator;

    public ClientGenerator(IJavaCodeParser parser, IRequestMethodGenerator methodGenerator)
    {
        _parser = parser ?? throw new ArgumentNullException(nameof(parser));
        _methodGenerator = methodGenerator ?? throw new ArgumentNullException(nameof(methodGenerator));
    }

    public void GenerateClient(string pathToControllers, string pathToProject, string rootNamespace, string baseUrl)
    {
        Directory.CreateDirectory(Path.Combine(pathToProject, "GeneratedClient"));
        var controllerInfo = _parser.ParseAllControllers(pathToControllers);

        foreach (var controller in controllerInfo)
            GenerateClientClass(controller, pathToProject, rootNamespace, baseUrl);
    }

    private void GenerateClientClass(
        ControllerModel controllerData,
        string pathToProject, 
        string rootNamespace, 
        string baseUrl)
    {
        var newNamespace = rootNamespace + "." + "GeneratedClient";
        
        var tree = SyntaxTree(CompilationUnit()
                .WithUsings(List(new []{
                    UsingDirective(QualifiedName(QualifiedName(
                        QualifiedName(
                            IdentifierName("System"),
                            IdentifierName("Net")),
                            IdentifierName("Http")),
                            IdentifierName("Json"))),
                    UsingDirective(QualifiedName(
                        QualifiedName(
                        IdentifierName("System"),
                        IdentifierName("Text")),
                        IdentifierName("Json"))),
                    UsingDirective(QualifiedName(
                        IdentifierName("System"),
                        IdentifierName("Web"))),
                    UsingDirective(QualifiedName(
                        IdentifierName(rootNamespace),
                        IdentifierName("GeneratedModels")))}))
                .WithMembers(SingletonList<MemberDeclarationSyntax>(
                FileScopedNamespaceDeclaration(IdentifierName(newNamespace))
                .WithMembers(SingletonList<MemberDeclarationSyntax>(
                ClassDeclaration(controllerData.Name)
                    .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                    .WithMembers(new SyntaxList<MemberDeclarationSyntax>(GenerateMembers(controllerData, baseUrl)))))))
                .NormalizeWhitespace());

        File.WriteAllText(
            Path.Combine(pathToProject, "GeneratedClient", $"{controllerData.Name}.cs"), tree.GetText().ToString());
    }

    private MemberDeclarationSyntax[] GenerateMembers(ControllerModel controllerData, string baseUrl)
    {
        var memberList = new List<MemberDeclarationSyntax>
        {
            FieldDeclaration(VariableDeclaration(IdentifierName("HttpClient"))
                    .WithVariables(SingletonSeparatedList(VariableDeclarator(
                            Identifier("_httpClient"))
                        .WithInitializer(EqualsValueClause(ObjectCreationExpression(
                                IdentifierName("HttpClient"))
                            .WithArgumentList(ArgumentList()))))))
                .WithModifiers(TokenList(new[]
                {
                    Token(SyntaxKind.PrivateKeyword),
                    Token(SyntaxKind.ReadOnlyKeyword)
                })),
            FieldDeclaration(VariableDeclaration(
                        IdentifierName("JsonSerializerOptions"))
                    .WithVariables(SingletonSeparatedList(
                        VariableDeclarator(Identifier("_serializerOptions"))
                            .WithInitializer(EqualsValueClause(ObjectCreationExpression(
                                    IdentifierName("JsonSerializerOptions"))
                                .WithInitializer(InitializerExpression(SyntaxKind.ObjectInitializerExpression,
                                    SingletonSeparatedList<ExpressionSyntax>(
                                        AssignmentExpression(
                                            SyntaxKind.SimpleAssignmentExpression,
                                            IdentifierName("PropertyNameCaseInsensitive"),
                                            LiteralExpression(SyntaxKind.TrueLiteralExpression))))))))))
                .WithModifiers(TokenList(new[]
                {
                    Token(SyntaxKind.PrivateKeyword),
                    Token(SyntaxKind.ReadOnlyKeyword)
                }))
        };
        memberList.AddRange(
            controllerData.MethodModels.Select(controllerDataMethodModel
                => _methodGenerator.GenerateMethod($"{baseUrl}/{controllerData.BaseUrl}", controllerDataMethodModel)));

        return memberList.ToArray();
    }
}