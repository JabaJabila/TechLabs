using JavaParser.SemanticDataModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Codegen.ClientGenerators;

public interface IRequestMethodGenerator
{
    MethodDeclarationSyntax GenerateMethod(string baseUrl, RequestMethodModel methodModel);
}