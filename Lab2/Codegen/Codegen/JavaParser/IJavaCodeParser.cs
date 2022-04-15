using Codegen.JavaParser.SemanticDataModels;

namespace Codegen.JavaParser;

public interface IJavaCodeParser
{
    IReadOnlyCollection<RequestModel> ParseAllRequestModels(string pathToFolder);
    IReadOnlyCollection<ControllerModel> ParseAllControllers(string pathToFolder);
}