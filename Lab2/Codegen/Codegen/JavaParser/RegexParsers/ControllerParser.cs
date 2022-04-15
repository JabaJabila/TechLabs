using System.Text.RegularExpressions;
using Codegen.JavaParser.SemanticDataModels;
using Codegen.JavaParser.Tools;

namespace Codegen.JavaParser.RegexParsers;

public class ControllerParser : IControllerParser
{
    private readonly IMethodInfoParser _methodInfoParser;
    private readonly Regex _classRegex = new Regex(@"class\s+\w+");
    private readonly Regex _requestMapping = new Regex(@"@RequestMapping");

    public ControllerParser(IMethodInfoParser methodInfoParser)
    {
        _methodInfoParser = methodInfoParser ?? throw new ArgumentNullException(nameof(methodInfoParser));
    }

    public ControllerModel GetControllerModel(string pathToFile)
    {
        var result = new ControllerModel();
        
        var fileStrings = File.ReadAllLines(pathToFile);
        var match = fileStrings.FirstOrDefault(s => _classRegex.IsMatch(s));
        if (match is null)
            throw new ParserException("Class name not found");

        match = _classRegex.Match(match).Value;
        result.Name = match[(match.LastIndexOf(' ') + 1)..];
        
        match = fileStrings.FirstOrDefault(s => _requestMapping.IsMatch(s));
        if (match is null)
            throw new ParserException("Request mapping not found");
        
        result.BaseUrl = match[(match.IndexOf('\"') + 1)..match.LastIndexOf('\"')];
        _methodInfoParser.FillMethodData(pathToFile, result);
            
        return result;
    }
}