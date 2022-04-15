using Microsoft.CodeAnalysis;

namespace Codegen;

public interface IClientGenerator
{
    IReadOnlyCollection<(SyntaxTree, string)> GenerateClient(
        string pathToControllers, 
        string pathToProject, 
        string rootNamespace, 
        string baseUrl);
}