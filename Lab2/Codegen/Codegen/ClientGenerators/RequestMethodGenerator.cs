using JavaParser.SemanticDataModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Codegen.ClientGenerators;

public class RequestMethodGenerator : IRequestMethodGenerator
{
    public MethodDeclarationSyntax GenerateMethod(string baseUrl, RequestMethodModel methodModel)
    {
        throw new NotImplementedException();
    }
}