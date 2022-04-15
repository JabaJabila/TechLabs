using Microsoft.CodeAnalysis;

namespace Codegen;

public interface IRequestModelGenerator
{
    IReadOnlyCollection<(SyntaxTree, string)> GenerateModels(string pathToModels, string pathToProject, string rootNamespace);
}