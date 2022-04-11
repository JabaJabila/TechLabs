namespace JavaParser;

public class JavaCodeParser
{
    private readonly IControllerParser _controllerParser;
    private readonly IDtoParser _dtoParser;

    public JavaCodeParser(IControllerParser controllerParser, IDtoParser dtoParser)
    {
        _controllerParser = controllerParser ?? throw new ArgumentNullException(nameof(controllerParser));
        _dtoParser = dtoParser ?? throw new ArgumentNullException(nameof(dtoParser));
    }
}