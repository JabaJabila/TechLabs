namespace Codegen;

public interface IRequestModelGenerator
{
    void GenerateModels(string pathToModels, string pathToProject, string rootNamespace);
}