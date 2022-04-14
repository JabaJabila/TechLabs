﻿using JavaParser;
using JavaParser.SemanticDataModels;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Codegen.ModelGenerators;

public class RequestModelGenerator : IRequestModelGenerator
{
    private const string NamespaceLastName = "GeneratedModels";
    private readonly IJavaCodeParser _parser;

    public RequestModelGenerator(IJavaCodeParser parser)
    {
        _parser = parser ?? throw new ArgumentNullException(nameof(parser));
    }

    public void GenerateModels(string pathToModels, string pathToProject, string rootNamespace)
    {
        Directory.CreateDirectory(Path.Combine(pathToProject, NamespaceLastName));
        var modelInfo = _parser.ParseAllRequestModels(pathToModels);
        foreach (var requestModel in modelInfo)
            GenerateModel(requestModel, pathToProject, rootNamespace);
    }

    private void GenerateModel(RequestModel modelData, string pathToProject, string rootNamespace)
    {
        var newNamespace = rootNamespace + "." + NamespaceLastName;
        
        var tree = SyntaxTree(CompilationUnit()
            .WithMembers(SingletonList<MemberDeclarationSyntax>(
                FileScopedNamespaceDeclaration(IdentifierName(newNamespace))
            .WithMembers(
                SingletonList<MemberDeclarationSyntax>(ClassDeclaration(modelData.ModelName)
                .WithModifiers(
                    TokenList(
                        Token(SyntaxKind.PublicKeyword)))
                .WithMembers(new SyntaxList<MemberDeclarationSyntax>(GenerateConstructorAndProperties(modelData)))))))
            .NormalizeWhitespace()
        );

        File.WriteAllText(
            Path.Combine(pathToProject, NamespaceLastName, $"{modelData.ModelName}.cs"), tree.GetText().ToString());
    }

    private MemberDeclarationSyntax[] GenerateConstructorAndProperties(RequestModel modelData)
    {
        var result = new List<MemberDeclarationSyntax>();

        result.Add(ConstructorDeclaration(Identifier(modelData.ModelName))
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(
                ParameterList(
                    SeparatedList<ParameterSyntax>(GetConstructorArgs(modelData))))
            .WithBody(GetConstructorBody(modelData)));

        var propList = GetProperties(modelData);
        result.AddRange(propList);

        return result.ToArray();
    }

    private List<PropertyDeclarationSyntax> GetProperties(RequestModel modelData)
              {
                  return modelData.PropertyNames.Zip(modelData.PropertyTypes, (name, type) =>
                  {
                      var prop = PropertyDeclaration(ParseTypeName(type), Identifier(name))
                          .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                          .WithAccessorList(
                              AccessorList(List(new []
                                      {
                                          AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                                              .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                                          
                                          AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                                              .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                                      })));
                      return prop;
                  }).ToList();
    }
    
    private SyntaxNodeOrToken[] GetConstructorArgs(RequestModel modelData)
    {
        var parameters = modelData.PropertyNames.Zip(
            modelData.PropertyTypes, (name, type) => 
                Parameter(Identifier(char.ToLower(name[0]) + name[1..]))
                .WithType(IdentifierName(type))).ToList();

        if (parameters.Count * 2 - 1 <= 0) return Array.Empty<SyntaxNodeOrToken>();
        var result = new SyntaxNodeOrToken[parameters.Count * 2 - 1];
        for (int i = 0; i < result.Length; i++)
            result[i] = i % 2 == 0 ? parameters[i / 2] : Token(SyntaxKind.CommaToken);

        return result;
    }

    private BlockSyntax GetConstructorBody(RequestModel modelData)
    {
        return Block(modelData.PropertyNames.Select(name => 
            (StatementSyntax)ExpressionStatement(AssignmentExpression(
                SyntaxKind.SimpleAssignmentExpression,
                IdentifierName(name), IdentifierName(char.ToLower(name[0]) + name[1..])))).ToArray());
    }
}