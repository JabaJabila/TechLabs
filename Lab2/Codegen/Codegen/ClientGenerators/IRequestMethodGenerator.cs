using Microsoft.CodeAnalysis.CSharp.Syntax;
using RequestMethodModel = Codegen.JavaParser.SemanticDataModels.RequestMethodModel;

namespace Codegen.ClientGenerators;

public interface IRequestMethodGenerator
{
    MethodDeclarationSyntax GenerateMethod(string baseUrl, RequestMethodModel methodModel);
}