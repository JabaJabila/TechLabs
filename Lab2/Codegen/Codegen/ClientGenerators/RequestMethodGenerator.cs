using JavaParser.SemanticDataModels;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Codegen.ClientGenerators;

public class RequestMethodGenerator : IRequestMethodGenerator
{
    public MethodDeclarationSyntax GenerateMethod(string baseUrl, RequestMethodModel methodModel)
    {
        return MethodDeclaration(
                ParseTypeName(methodModel.ReturnType == "void" ? "Task" : $"Task<{methodModel.ReturnType}>"),
                Identifier(methodModel.Name))
            .WithModifiers(
                TokenList(new[] {Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.AsyncKeyword)}))
            .WithParameterList(
                ParameterList(SeparatedList<ParameterSyntax>(GetMethodArguments(methodModel))))
            .WithBody(
                Block(GenerateBody(baseUrl, methodModel)));
    }
    
    private SyntaxNodeOrToken[] GetMethodArguments(RequestMethodModel methodModel)
    {
        var parameters = methodModel.Arguments.Select(a =>
            Parameter(Identifier(a.Name)).WithType(IdentifierName(a.Type))).ToList();

        if (parameters.Count * 2 - 1 <= 0) return Array.Empty<SyntaxNodeOrToken>();
        var result = new SyntaxNodeOrToken[parameters.Count * 2 - 1];
        for (int i = 0; i < result.Length; i++)
            result[i] = i % 2 == 0 ? parameters[i / 2] : Token(SyntaxKind.CommaToken);

        return result;
    }
    
    private StatementSyntax[] GenerateBody(string baseUrl, RequestMethodModel methodModel)
    {
        throw new NotImplementedException();
    }
}