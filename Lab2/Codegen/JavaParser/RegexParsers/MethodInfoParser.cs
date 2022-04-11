using System.Text.RegularExpressions;
using JavaParser.SemanticDataModels;

namespace JavaParser.RegexParsers;

public class MethodInfoParser : IMethodInfoParser
{
    // TODO
    private readonly Regex _methodPattern = new Regex(@"\w+\s+\w+<?.+>?\s+\w+\s*\((.*\s*,?)*\)", RegexOptions.Multiline);

    public RequestMethodModel GetMethodData(string pathToFile)
    {
        // TODO
        return null;
    }
}