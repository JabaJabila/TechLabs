using JavaParser.SemanticDataModels;
using JavaParser.Tools;
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
                Block(GenerateBody(methodModel, baseUrl)));
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
    
    private StatementSyntax[] GenerateBody(RequestMethodModel methodModel, string baseUrl)
    {
        var statements = new List<StatementSyntax>();
        var queryArgs = methodModel.Arguments.Where(a => a.RequestType == RequestArgumentType.Query).ToList();
        if (queryArgs.Count > 0)
        {
            statements.Add(
                LocalDeclarationStatement(VariableDeclaration(IdentifierName(
                        Identifier(TriviaList(), SyntaxKind.VarKeyword, "var", "var", TriviaList())))
                    .WithVariables(SingletonSeparatedList(
                        VariableDeclarator(Identifier("query"))
                            .WithInitializer(EqualsValueClause(InvocationExpression(
                                    MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, 
                                        IdentifierName("HttpUtility"),
                                        IdentifierName("ParseQueryString")))
                                .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(
                                    MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                        PredefinedType(Token(SyntaxKind.StringKeyword)),
                                        IdentifierName("Empty"))))))))))));
            
            queryArgs.ForEach(a =>
            {
                statements.Add(ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression, ElementAccessExpression(
                                IdentifierName("query"))
                            .WithArgumentList(
                                BracketedArgumentList(SingletonSeparatedList(
                                    Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(a.Name)))))),
                                            InvocationExpression(MemberAccessExpression(
                                                    SyntaxKind.SimpleMemberAccessExpression, 
                                                    PredefinedType(Token(SyntaxKind.StringKeyword)),
                                                    IdentifierName("Join")))
                                            .WithArgumentList(
                                                ArgumentList(SeparatedList<ArgumentSyntax>(
                                                    new SyntaxNodeOrToken[]{
                                                            Argument(LiteralExpression(
                                                                    SyntaxKind.CharacterLiteralExpression,
                                                                    Literal(','))),
                                                            Token(SyntaxKind.CommaToken),
                                                            Argument(IdentifierName(a.Name))}))))));
            });
        }
        else
        {
            statements.Add(
                LocalDeclarationStatement(
                    VariableDeclaration(IdentifierName(Identifier(TriviaList(), SyntaxKind.VarKeyword,
                                    "var", "var", TriviaList())))
                        .WithVariables(SingletonSeparatedList(VariableDeclarator(Identifier("query"))
                                    .WithInitializer(EqualsValueClause(MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                PredefinedType(Token(SyntaxKind.StringKeyword)),
                                                IdentifierName("Empty"))))))));
        }


        if (methodModel.RequestType == RequestType.Post)
        {
            var bodyArg = methodModel.Arguments.FirstOrDefault(a => a.RequestType == RequestArgumentType.Body);
            var identifier = "\"\"";
            if (bodyArg is not null) identifier = bodyArg.Name;
            
            statements.Add(LocalDeclarationStatement(
                    VariableDeclaration(IdentifierName(
                                Identifier(TriviaList(), SyntaxKind.VarKeyword, "var", "var", TriviaList())))
                        .WithVariables(
                            SingletonSeparatedList(VariableDeclarator(Identifier("content"))
                                    .WithInitializer(EqualsValueClause(
                                            InvocationExpression(MemberAccessExpression(
                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                        IdentifierName("JsonContent"),
                                                        IdentifierName("Create")))
                                                .WithArgumentList(ArgumentList(
                                                        SingletonSeparatedList(
                                                            Argument(IdentifierName(identifier)))))))))));
        }
        
        statements.Add(
            LocalDeclarationStatement(VariableDeclaration(IdentifierName(
                    Identifier(TriviaList(), SyntaxKind.VarKeyword, "var", "var", TriviaList())))
                .WithVariables(SingletonSeparatedList(VariableDeclarator(Identifier("response"))
                    .WithInitializer(EqualsValueClause(
                        AwaitExpression(InvocationExpression(MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression, 
                                IdentifierName("_httpClient"),
                                IdentifierName(SelectRequestType(methodModel))))
                            .WithArgumentList(ArgumentList(SeparatedList<ArgumentSyntax>(
                                                                GetRequestCallArguments(methodModel, baseUrl)))))))))));

        if (methodModel.ReturnType != "void")
        {
            statements.Add(ReturnStatement(BinaryExpression(SyntaxKind.CoalesceExpression,
                InvocationExpression(MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression, IdentifierName("JsonSerializer"),
                        GenericName(Identifier("Deserialize"))
                            .WithTypeArgumentList(TypeArgumentList(
                                SingletonSeparatedList<TypeSyntax>(
                                    IdentifierName(methodModel.ReturnType))))))
                    .WithArgumentList(ArgumentList(SeparatedList<ArgumentSyntax>(new SyntaxNodeOrToken[]{
                        Argument(AwaitExpression(InvocationExpression(MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression, MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression, IdentifierName("response"),
                                IdentifierName("Content")), IdentifierName("ReadAsStringAsync"))))),
                        Token(SyntaxKind.CommaToken), Argument(IdentifierName("_serializerOptions"))}))),
                ThrowExpression(ObjectCreationExpression(IdentifierName("InvalidOperationException"))
                    .WithArgumentList(ArgumentList(SingletonSeparatedList(
                        Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, 
                            Literal("Request returned null"))))))))));
        }
        
        return statements.ToArray();
    }

    private string SelectRequestType(RequestMethodModel methodModel)
    {
        return methodModel.RequestType switch
        {
            RequestType.Get => "GetAsync",
            RequestType.Post => "PostAsync",
            RequestType.Patch => "PatchAsync",
            RequestType.Put => "PutAsync",
            RequestType.Delete => "DeleteAsync",
            _ => throw new NotSupportedException("Request type not supported"),
        };
    }

    private SyntaxNodeOrToken[] GetRequestCallArguments(RequestMethodModel methodModel, string baseUrl)
    {
        var result = new List<SyntaxNodeOrToken>
        {
            Argument(
                BinaryExpression(SyntaxKind.AddExpression,  
                    InterpolatedStringExpression(
                            Token(SyntaxKind.InterpolatedStringStartToken))
                        .WithContents(SingletonList<InterpolatedStringContentSyntax>(InterpolatedStringText()
                            .WithTextToken(Token(TriviaList(),
                                SyntaxKind.InterpolatedStringTextToken, 
                                $"{baseUrl}/{methodModel.Url}",
                                $"{baseUrl}/{methodModel.Url}",
                                TriviaList())))),
                    InterpolatedStringExpression(Token(SyntaxKind.InterpolatedStringStartToken))
                        .WithContents(List(
                            new InterpolatedStringContentSyntax[]{
                                InterpolatedStringText().WithTextToken(
                                    Token(TriviaList(),
                                        SyntaxKind.InterpolatedStringTextToken, "?", "?", TriviaList())),
                                Interpolation(IdentifierName("query"))}))))
        };


        if (methodModel.RequestType != RequestType.Post) return result.ToArray();
        result.Add(Token(SyntaxKind.CommaToken));
        result.Add(Argument(IdentifierName("content")));

        return result.ToArray();
    }
}