using JavaParser.SemanticDataModels;

namespace JavaParser.RegexParsers;

public class ControllerParser : IControllerParser
{
    private readonly IMethodInfoParser _methodInfoParser;
    private readonly IRequestRouteParser _requestRouteParser;

    public ControllerParser(IMethodInfoParser methodInfoParser, IRequestRouteParser requestRouteParser)
    {
        _methodInfoParser = methodInfoParser ?? throw new ArgumentNullException(nameof(methodInfoParser));
        _requestRouteParser = requestRouteParser ?? throw new ArgumentNullException(nameof(requestRouteParser));
    }

    public ControllerModel GetControllerModel(string path)
    {
        // TODO
        return null;
    }
}