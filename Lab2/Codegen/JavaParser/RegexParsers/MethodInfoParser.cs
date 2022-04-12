using System.Text.RegularExpressions;
using JavaParser.SemanticDataModels;
using JavaParser.Tools;

namespace JavaParser.RegexParsers;

public class MethodInfoParser : IMethodInfoParser
{
    private readonly Regex _methodPattern = new Regex(@"\w+\s+\w+<?.+>?\s+\w+\s*\((.*\s*,?)*\)");
    private readonly IJavaToCSharpTypeMapper _typeMapper;

    public MethodInfoParser(IJavaToCSharpTypeMapper typeMapper)
    {
        _typeMapper = typeMapper ?? throw new ArgumentNullException(nameof(typeMapper));
    }

    public RequestMethodModel GetMethodData(string pathToFile)
    {
        // TODO
        return null;
    }
}