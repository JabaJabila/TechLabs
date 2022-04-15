namespace Codegen;

public interface IClientGenerator
{
    void GenerateClient(string pathToControllers, string pathToProject, string rootNamespace, string baseUrl);
}