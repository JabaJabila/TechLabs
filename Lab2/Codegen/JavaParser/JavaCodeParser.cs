namespace JavaParser;

public class JavaCodeParser
{
    private readonly IControllerParser _controllerParser;
    private readonly IRequestModelParser _requestModelParser;

    public JavaCodeParser(IControllerParser controllerParser, IRequestModelParser requestModelParser)
    {
        _controllerParser = controllerParser ?? throw new ArgumentNullException(nameof(controllerParser));
        _requestModelParser = requestModelParser ?? throw new ArgumentNullException(nameof(requestModelParser));
    }
}