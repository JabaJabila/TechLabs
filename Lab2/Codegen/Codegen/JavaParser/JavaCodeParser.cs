using Codegen.JavaParser.SemanticDataModels;

namespace Codegen.JavaParser;

public class JavaCodeParser : IJavaCodeParser
{
    private readonly IControllerParser _controllerParser;
    private readonly IRequestModelParser _requestModelParser;

    public JavaCodeParser(IControllerParser controllerParser, IRequestModelParser requestModelParser)
    {
        _controllerParser = controllerParser ?? throw new ArgumentNullException(nameof(controllerParser));
        _requestModelParser = requestModelParser ?? throw new ArgumentNullException(nameof(requestModelParser));
    }

    public IReadOnlyCollection<RequestModel> ParseAllRequestModels(string pathToFolder)
    {
        return Directory
            .GetFiles(pathToFolder)
            .Select(fileName => _requestModelParser.GetModelInfo(fileName))
            .ToList();
    }

    public IReadOnlyCollection<ControllerModel> ParseAllControllers(string pathToFolder)
    {
        return Directory
            .GetFiles(pathToFolder)
            .Select(fileName => _controllerParser.GetControllerModel(fileName))
            .ToList();
    }
}