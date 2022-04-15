using JavaParser.SemanticDataModels;

namespace JavaParser;

public interface IJavaCodeParser
{
    IReadOnlyCollection<RequestModel> ParseAllRequestModels(string pathToFolder);
    IReadOnlyCollection<ControllerModel> ParseAllControllers(string pathToFolder);
}